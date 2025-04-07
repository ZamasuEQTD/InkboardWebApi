using Domain.Core;

namespace Domain.Hilos.DomainEvents
{
    public class HiloPosteadoDomainEvent : IDomainEvent
    {
        public Guid HiloId { get; }
        public HiloPosteadoDomainEvent(  Guid hiloId)
        {
            HiloId = hiloId;
        }

    }
}