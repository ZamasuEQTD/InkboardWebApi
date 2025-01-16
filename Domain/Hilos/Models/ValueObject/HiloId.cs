using Domain.Core;

namespace Domain.Hilos.Models.ValueObjects
{
    public class HiloId : EntityId
    {
        private HiloId() { }
        public HiloId(Guid id) : base(id) { }
    }
}