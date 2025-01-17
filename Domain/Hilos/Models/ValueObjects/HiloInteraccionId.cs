using Domain.Core;

namespace Domain.Hilos.Models.ValueObjects
{
    public class HiloInteraccionId : EntityId
    {
        private HiloInteraccionId() { }
        public HiloInteraccionId(Guid id) : base(id) { }
    }
}