using Domain.Core.Abstractions;
using Domain.Hilos.Models.ValueObject;
using Domain.Hilos.Models.ValueObjects;

namespace Domain.Hilos.Models
{
    public class Sticky : Entity<StickyId>
    {
        public HiloId HiloId { get; private set; }

        public Sticky(HiloId hilo )
        {
            this.Id = new(Guid.NewGuid());
            this.HiloId = hilo;
        }

        private Sticky(){}
    }
}