using Domain.Core;

namespace Domain.Comentarios.Models.ValueObjects
{
    public class RespuestaId : EntityId
    {
        public RespuestaId(Guid id) : base(id) { }
        public RespuestaId() : base() { }
    }
}