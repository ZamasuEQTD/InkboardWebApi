using Domain.Core;

namespace Domain.Comentarios.Models.ValueObjects
{
    public class ComentarioId : EntityId
    {
        public ComentarioId(Guid id) : base(id) { }
        private ComentarioId() { }
    }
}