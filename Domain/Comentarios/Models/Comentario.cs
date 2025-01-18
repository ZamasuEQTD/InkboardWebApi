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
        public ICollection<ComentarioInterracion> Relaciones { get; private set; } = [];
        public ICollection<RespuestaComentario> Respuestas { get; private set; } = [];


        public void Eliminar(Hilo hilo){
            if(hilo.EstaEliminado) throw new DomainBusinessException("El hilo esta eliminado");

            if(EstaEliminado) throw new DomainBusinessException("El comentario esta eliminado");

        }


        public bool EstaEliminado => this.Status == ComentariosStatus.Eliminado;
        public bool EsAutor(IdentityId usuarioId) => AutorId == usuarioId;
    }
}