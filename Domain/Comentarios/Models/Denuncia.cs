using Domain.Comentarios.Models.ValueObjects;
using Domain.Denuncias;
using Domain.Denuncias.Models;
using Domain.Usuarios;
using Domain.Usuarios.Models.ValueObjects;

namespace Domain.Comentarios.Models
{
    public class DenunciaDeComentario : Denuncia
    {
        public ComentarioId ComentarioId { get; private set; }
        public RazonDeDenuncia Razon { get; private set; }
        private DenunciaDeComentario() { }
        public DenunciaDeComentario(IdentityId denuncianteId, ComentarioId comentarioId, RazonDeDenuncia razon) : base(denuncianteId)
        {
            ComentarioId = comentarioId;
            Razon = razon;
        }

        public enum RazonDeDenuncia
        {
            Otro
        }
    }
}