namespace Domain.Core.Abstractions
{
     public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public ICollection<IDomainEvent> DomainEvents => _domainEvents;
        protected void Raise(IDomainEvent IDomainEvent)
        {
            _domainEvents.Add(IDomainEvent);
        }


        protected Entity() { }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }

    public abstract class Entity<TId> : Entity where TId : EntityId
    {
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public TId Id { get; protected set; }
        protected Entity() { }
        protected Entity(TId id) : base()
        {
            Id = id;
        }
    }

}