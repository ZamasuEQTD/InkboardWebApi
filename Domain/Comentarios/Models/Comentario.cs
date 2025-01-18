using Domain.Comentarios.Models.Enums;
using Domain.Comentarios.Models.ValueObjects;
using Domain.Core;
using Domain.Core.Abstractions;
using Domain.Hilos.Models;
using Domain.Hilos.Models.ValueObjects;
using Domain.Usuarios.Models;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Comentarios.Models
{
    public class Comentario : Entity<ComentarioId>
    {
        public bool RecibirNotificaciones { get; private set; }
        public ComentariosStatus Status {get; private set;}
        public HiloId HiloId {get; private set;}
        public IdentityId AutorId { get; private set; }
        public Color Color {get; private set;}
        public Texto Texto { get; private set; }
        public Tag Tag { get; private set; }
        public Dados? Dados { get; private set; }
        public TagUnico? TagUnico { get; private set; }
        public ICollection<DenunciaDeComentario> Denuncias { get; private set; } = [];
        public ICollection<ComentarioInterracion> Interaciones { get; private set; } = [];
        public ICollection<RespuestaComentario> Respuestas { get; private set; } = [];

        public void Eliminar(Hilo hilo){
            if(hilo.EstaEliminado) throw new DomainBusinessException("El hilo esta eliminado");

            if(EstaEliminado) throw new DomainBusinessException("El comentario esta eliminado");

            DesestimarDenuncias();

            this.Status = ComentariosStatus.Eliminado;
        }

        public void AgregarRespuesta(ComentarioId respuesta)
        {
            if (EstaActivo)
            {
                Respuestas.Add(new RespuestaComentario(Id, respuesta));
            }
        }

        public void Denunciar(Hilo hilo, IdentityId usuarioId)
        {
            if(hilo.EstaEliminado) throw new DomainBusinessException("el hil está eliminado");

            if (HaDenunciado(usuarioId)) throw new DomainBusinessException("Ya has denunciado este comentario");

            Denuncias.Add(new DenunciaDeComentario(usuarioId, Id, DenunciaDeComentario.RazonDeDenuncia.Otro));
        }

        private void DesestimarDenuncias (){ 
            foreach (var d in this.Denuncias)
            {
                if(!d.EstaDesestimada) d.Desestimar();  
            }
        }

        public void Ocultar(Hilo hilo, IdentityId usuarioId)
        {
            if (!hilo.EstaActivo) throw new DomainBusinessException("El hilo esta inactivo");

            if (!EstaActivo) throw new DomainBusinessException("El comentario esta inactivo");

            ComentarioInterracion? interaccion = GetInterracionDeUsuario(usuarioId);

            if (interaccion is null)
            {
                interaccion = new ComentarioInterracion(Id, usuarioId);
                
                Interaciones.Add(interaccion);
            }

            interaccion.Ocultar();
        }
        public ComentarioInterracion? GetInterracionDeUsuario(IdentityId usuario) => this.Interaciones.FirstOrDefault(i=> i.UsuarioId == usuario);
        public bool EstaEliminado => this.Status == ComentariosStatus.Eliminado;
        public bool EstaActivo => this.Status == ComentariosStatus.Activo;
        public bool HaDenunciado(IdentityId usuarioId) => Denuncias.Any(d => d.DenuncianteId == usuarioId);
        public bool EsAutor(IdentityId usuarioId) => AutorId == usuarioId;
    }
}