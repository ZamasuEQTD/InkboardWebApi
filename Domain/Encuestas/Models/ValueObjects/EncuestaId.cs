using Domain.Core;

namespace Domain.Encuestas.Models.ValueObjects
{
    public class EncuestaId : EntityId
    {
        public EncuestaId(Guid id) : base(id) { }
        private EncuestaId() { }
    }
}