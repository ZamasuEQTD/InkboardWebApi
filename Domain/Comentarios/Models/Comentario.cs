using Domain.Comentarios.Models.Enums;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Core.Models;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Media.Models.ValueObjects;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Comentarios.Models
{
    public class Comentario : Entity<ComentarioId>
    {
        public bool RecibirNotificaciones { get; private set; }
        public ComentariosStatus Status { get; private set; }
        public HiloId HiloId { get; private set; }
        public IdentityId AutorId { get; private set; }
        public Color Color { get; private set; }
        public AutorRole Autor {get; private set;}
        public Texto Texto { get; private set; }
        public string Tag { get; private set; }
        public int? Dados { get; private set; }
        public string? TagUnico { get; private set; }
        public MediaSpoileableId? MediaId { get; private set; }
        public ICollection<DenunciaDeComentario> Denuncias { get; private set; } = [];
        public ICollection<ComentarioInterracion> Interaciones { get; private set; } = [];
        public ICollection<RespuestaComentario> Respuestas { get; private set; } = [];

        public Comentario(
            HiloId hiloId,
            IdentityId autorId,
            Color color,
            Texto texto,
            AutorRole autor,
            string tag,
            MediaSpoileableId? mediaId = null,
            int? dados = null,
            string? tagUnico = null
        )
            : base()
        {
            Id = new ComentarioId(Guid.NewGuid());
            HiloId = hiloId;
            AutorId = autorId;
            Color = color;
            Texto = texto;
            Tag = tag;
            RecibirNotificaciones = true;
            MediaId = mediaId;
            Dados = dados;
            TagUnico = tagUnico;
            Status = ComentariosStatus.Activo;
            Autor = autor;
        }
        private Comentario() { }


        public Result Eliminar(Hilo hilo)
        {
            if (hilo.EstaEliminado) return Result.Failure(ComentarioErrors.HiloEliminado);

            if (EstaEliminado) return Result.Failure(ComentarioErrors.ComentarioYaEliminado);

            DesestimarDenuncias();

            this.Status = ComentariosStatus.Eliminado;
            
            return Result.Success();
        }

        public Result AgregarRespuesta(ComentarioId respuesta)
        {
            if (!EstaActivo) return Result.Failure(ComentarioErrors.ComentarioInactivo);

            Respuestas.Add(new RespuestaComentario(Id, respuesta));
            
            return Result.Success();
        }

        public Result Denunciar(Hilo hilo, IdentityId usuarioId)
        {
            if (hilo.EstaEliminado) return Result.Failure(ComentarioErrors.HiloEliminado);

            if (HaDenunciado(usuarioId)) return Result.Failure(ComentarioErrors.YaDenunciado);

            Denuncias.Add(new DenunciaDeComentario(usuarioId, Id, DenunciaDeComentario.RazonDeDenuncia.Otro));
            return Result.Success();
        }

        private void DesestimarDenuncias()
        {
            foreach (var d in this.Denuncias)
            {
                if (!d.EstaDesestimada) d.Desestimar();
            }
        }

        public Result Ocultar(Hilo hilo, IdentityId usuarioId)
        {
            if (!hilo.EstaActivo) return Result.Failure(ComentarioErrors.HiloInactivo);

            if (!EstaActivo) return Result.Failure(ComentarioErrors.ComentarioInactivo);

            ComentarioInterracion? interaccion = GetInterracionDeUsuario(usuarioId);

            if (interaccion is null)
            {
                interaccion = new ComentarioInterracion(Id, usuarioId);
                Interaciones.Add(interaccion);
            }

            interaccion.Ocultar();
            return Result.Success();
        }

        public ComentarioInterracion? GetInterracionDeUsuario(IdentityId usuario) => this.Interaciones.FirstOrDefault(i => i.UsuarioId == usuario);
        public bool EstaEliminado => this.Status == ComentariosStatus.Eliminado;
        public bool EstaActivo => this.Status == ComentariosStatus.Activo;
        public bool HaDenunciado(IdentityId usuarioId) => Denuncias.Any(d => d.DenuncianteId == usuarioId);
        public bool EsAutor(IdentityId usuarioId) => AutorId == usuarioId;
    }

    public static class ComentarioErrors
    {
        public static readonly Error NoEncontrado = new("NoEncontrado", "El comentario no ha sido encontrado.");
        
        public static readonly Error HiloEliminado = new("HiloEliminado", "El hilo está eliminado.");
        public static readonly Error ComentarioYaEliminado = new("ComentarioYaEliminado", "El comentario ya está eliminado.");
        public static readonly Error ComentarioInactivo = new("ComentarioInactivo", "El comentario está inactivo.");
        public static readonly Error YaDenunciado = new("YaDenunciado", "Ya has denunciado este comentario.");
        public static readonly Error HiloInactivo = new("HiloInactivo", "El hilo está inactivo.");
    }
}