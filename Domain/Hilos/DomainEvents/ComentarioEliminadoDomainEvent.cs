using Domain.Core;

namespace Domain.Hilos.DomainEvents
{
    public class ComentarioEliminadoDomainEvent : IDomainEvent{ 
        public Guid HiloId { get; set; }
        public string ComentarioTag { get; set; }
    }
}