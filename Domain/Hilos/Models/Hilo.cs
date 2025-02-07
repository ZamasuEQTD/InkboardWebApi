using Domain.Categorias.Models.ValueObjects;
using Domain.Comentarios.Models;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Comentarios.Utils;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Core.Models;
using Domain.Denuncias.Models;
using Domain.Encuestas.Models.ValueObjects;
using Domain.Hilos.Models.Enums;
using Domain.Hilos.Models.ValueObjects;
using Domain.Media.Models.ValueObjects;
using Domain.Notificaciones;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Hilos.Models
{
    public class Hilo : Entity<HiloId>
    {
        public IdentityId AutorId { get; private set; }
        public Sticky? Sticky { get; private set; }
        public HiloStatus Status { get; private set; }
        public DateTime UltimoBump { get; private set; }
        public string Titulo { get; private set; }
        public string Descripcion { get; private set; }
        public bool RecibirNotificaciones { get; private set; }
        public ConfiguracionDeComentarios Configuracion { get; private set; }
        public AutorRole Autor {get; private set;}
        public MediaSpoileableId PortadaId { get; private set; }
        public SubcategoriaId SubcategoriaId { get; private set; }
        public EncuestaId? EncuestaId { get; private set; }
        public ICollection<DenunciaHilo> Denuncias { get; private set; } = [];
        public ICollection<HiloInteraccion> Interacciones { get; private set; } = [];
        public ICollection<Comentario> Comentarios { get; private set; } = [];
        public ICollection<ComentarioDestacado> ComentariosDestacados { get; private set; } = [];
        public ICollection<Notificacion> Notificaciones { get; private set; } = [];
        public Hilo(
            IdentityId autorId,
            string titulo,
            string descripcion,
            SubcategoriaId subcategoriaId,
            MediaSpoileableId portadaId,
            AutorRole autor,
            EncuestaId? encuestaId,
            ConfiguracionDeComentarios configuracion
        )
            : base()
        {
            Id = new HiloId(Guid.NewGuid());
            AutorId = autorId;
            Titulo = titulo;
            Descripcion = descripcion;
            SubcategoriaId = subcategoriaId;
            PortadaId = portadaId;
            Autor = autor;
            Configuracion = configuracion;
            RecibirNotificaciones = true;
            Status = HiloStatus.Activo;
            UltimoBump = DateTime.UtcNow;
            EncuestaId = encuestaId;
        }

        private Hilo() { }

          public Result Comentar(Comentario comentario, DateTime now)
        {
            if (!EstaActivo) return HiloErrors.HiloInactivo;

            Comentarios.Add(comentario);

            if(!EsAutor(comentario.AutorId) && RecibirNotificaciones)
            {
                Notificaciones.Add(new HiloComentadoNotificacion(AutorId, Id, comentario.Id));
            }

            List<string> tags = TagUtils.GetTags(comentario.Texto.Value); 

            foreach (var tag in tags)
            {
                Comentario? respondido = Comentarios.FirstOrDefault(c => c.Tag == tag);

                if (respondido is not null)
                {
                    respondido.AgregarRespuesta(comentario.Id);

                    if(respondido.AutorId != comentario.AutorId && respondido.RecibirNotificaciones)
                    {
                        Notificacion notificacion = new ComentarioRespondidoNotificacion(
                            respondido.AutorId,
                            Id,
                            comentario.Id,
                            respondido.Id
                        );

                        Notificaciones.Add(notificacion);
                    }
                }
            }

            NotificarSeguidores(comentario);

            this.UltimoBump = now;

            return Result.Success();
        }

        private void NotificarSeguidores(Comentario comentario){
             List<IdentityId> seguidores = Interacciones.Where(i => i.Seguido).Select(i => i.UsuarioId).ToList();

            foreach (IdentityId seguidor in seguidores)
            {
                if(!comentario.EsAutor(seguidor)) {
                    Notificaciones.Add(new HiloSeguidoNotificacion(seguidor, Id, comentario.Id));
                }
            }
        }

        public Result DestacarComentario(IdentityId usuario, Comentario comentario)
        {
            if (!EstaActivo) return Result.Failure(HiloErrors.HiloInactivo);

            if (comentario.EstaEliminado) return Result.Failure(HiloErrors.ComentarioEliminado);

            if (!EsAutor(usuario)) return Result.Failure(HiloErrors.NoEsAutor);

            if (ComentarioEstaDestacado(comentario.Id))
            {
                this.ComentariosDestacados = [..ComentariosDestacados.Where(c => c.Id == comentario.Id)];
                return Result.Success();
            }

            if (HaAlcandoMaximaCantidadDeDestacados) return Result.Failure(HiloErrors.MaxComentariosDestacados);

            this.ComentariosDestacados.Add(new ComentarioDestacado(comentario.Id, Id));
            return Result.Success();
        }

        public Result Eliminar()
        {
            if (EstaEliminado) return Result.Failure(HiloErrors.HiloEliminado);

            if (TieneStickyActivo) EliminarSticky();

            DesestimarDenuncias();

            this.Status = HiloStatus.Eliminado;
            return Result.Success();
        }

        public Result EliminarSticky()
        {
            if (!TieneStickyActivo) return Result.Failure(HiloErrors.SinStickyActivo);

            this.Sticky = null;
            return Result.Success();
        }

        public Result EstablecerSticky()
        {
            if (TieneStickyActivo) return Result.Failure(HiloErrors.StickyActivo);

            this.Sticky = new Sticky(this.Id);
            return Result.Success();
        }

        public Result Denunciar(IdentityId usuario)
        {
            if (HaDenunciado(usuario)) return Result.Failure(HiloErrors.YaDenunciado);

            Denuncias.Add(new DenunciaHilo(usuario, Id));
            return Result.Success();
        }

        private void DesestimarDenuncias()
        {
            foreach (var denuncia in Denuncias)
            {
                if (!denuncia.EstaDesestimada)
                {
                    denuncia.Desestimar();
                }
            }
        }

        public Result RealizarInteraccion(HiloInteraccion.Acciones accion, IdentityId usuario)
        {
            if (EstaEliminado) return Result.Failure(HiloErrors.HiloEliminado);

            HiloInteraccion? interaccion = GetInteraccionDeUsuario(usuario);

            if (interaccion is null)
            {
                interaccion = new HiloInteraccion(Id, usuario);
                this.Interacciones.Add(interaccion);
            }

            interaccion.EjecutarAccion(accion);
            return Result.Success();
        }

        public Result CambiarSubcategoria(SubcategoriaId subcategoria)
        {
            this.SubcategoriaId = subcategoria;
            return Result.Success();
        }

        public Result CambiarNotificaciones(IdentityId usuario)
        {
            if (!EsAutor(usuario)) return Result.Failure(HiloErrors.NoEsAutor);

            this.RecibirNotificaciones = !this.RecibirNotificaciones;
            return Result.Success();
        }

        public HiloInteraccion? GetInteraccionDeUsuario(IdentityId usuario) => this.Interacciones.FirstOrDefault(i => i.UsuarioId == usuario);

        bool HaAlcandoMaximaCantidadDeDestacados => ComentariosDestacados.Count == 5;
        public bool TieneStickyActivo => this.Sticky is not null;
        public bool EstaEliminado => this.Status == HiloStatus.Eliminado;
        public bool EstaActivo => this.Status == HiloStatus.Activo;
        public bool ComentarioEstaDestacado(ComentarioId comentarioId) => this.ComentariosDestacados.Any(d => d.ComentarioId == comentarioId);
        public bool EsAutor(IdentityId usuarioId) => this.AutorId == usuarioId;
        public bool HaDenunciado(IdentityId usuarioId) => Denuncias.Any(d => d.DenuncianteId == usuarioId);
    }

    public static class HiloErrors
    {
        public static readonly Error NoEncontrado = new("NoEncontrado", "El hilo no fue encontrado.");
        public static readonly Error HiloInactivo = new("HiloInactivo", "El hilo está inactivo.");
        public static readonly Error ComentarioEliminado = new("ComentarioEliminado", "El comentario ha sido eliminado.");
        public static readonly Error NoEsAutor = new("NoEsAutor", "No eres el autor del hilo.");
        public static readonly Error MaxComentariosDestacados = new("MaxComentariosDestacados", "Se ha alcanzado el máximo de comentarios destacados.");
        public static readonly Error SinStickyActivo = new("SinStickyActivo", "No hay un sticky activo.");
        public static readonly Error StickyActivo = new("StickyActivo", "Ya hay un sticky activo.");
        public static readonly Error YaDenunciado = new("YaDenunciado", "Ya has denunciado este hilo.");
        public static readonly Error HiloEliminado = new("HiloEliminado", "El hilo ha sido eliminado.");
        public static readonly Error SinPortada = new("SinPortada", "El hilo no tiene portada.");
        public static readonly Error ArchivoDePortadaInvalida = new("ArchivoDePortadaInvalida", "El archivo de portada es inválido.");
    }
}