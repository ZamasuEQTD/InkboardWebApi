using Domain.Core;

namespace Domain.Hilos.DomainEvents
{
    public class ComentarioEliminadoDomainEvent : IDomainEvent{ 
        public Guid HiloId { get; set; }
        public Guid ComentarioId { get; set; }
    }
}