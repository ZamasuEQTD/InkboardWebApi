using Domain.Core;

namespace Domain.Hilos.Models.ValueObject
{
    public class StickyId : EntityId
    {
        private StickyId() { }
        public StickyId(Guid id) : base(id) { }
    }
}