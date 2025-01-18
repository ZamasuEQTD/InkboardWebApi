using Domain.Comentarios.Models.ValueObjects;
using Domain.Core.Abstractions;
using Domain.Hilos;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Comentarios.Models
{
    public class ComentarioInterracion : Entity<ComentarioInterracionId>
    {
        public ComentarioId ComentarioId { get; private set; }
        public IdentityId UsuarioId { get; private set; }
        public bool Oculto { get; private set; }
        private ComentarioInterracion() { }
        public ComentarioInterracion(ComentarioId comentarioId, IdentityId usuarioId)
        {
            this.Id = new(Guid.NewGuid());
            this.ComentarioId = comentarioId;
            this.UsuarioId = usuarioId;
            this.Oculto = false;
        }

        public void Ocultar()
        {
            this.Oculto = !this.Oculto;
        }
    }
}