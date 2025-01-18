using Domain.Core;

namespace Domain.Comentarios.Models.ValueObjects
{
    public class ComentarioInterracionId : EntityId
    {
        public ComentarioInterracionId(Guid id) : base(id) { }
        private ComentarioInterracionId() { }
    }
}