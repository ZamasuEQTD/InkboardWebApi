using Domain.Core;

namespace Domain.Hilos.DomainEvents
{
    public class HiloEliminadoDomainEvent : IDomainEvent
    {
        public Guid HiloId { get; }

        public HiloEliminadoDomainEvent(Guid hiloId)
        {
            HiloId = hiloId;
        }
    }
}