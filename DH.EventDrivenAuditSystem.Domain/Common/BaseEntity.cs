namespace DH.EventDrivenAuditSystem.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; init; }

        private readonly List<IDomainEvent> _domainEvents = new();

        /// <summary>
        /// Gets all domain events raised by this entity.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
            => _domainEvents.AsReadOnly();

        /// <summary>
        /// Clears all domain events after they have been published.
        /// </summary>
        public void ClearDomainEvents()
            => _domainEvents.Clear();

        /// <summary>
        /// Raises a domain event within the aggregate.
        /// </summary>
        protected void RaiseDomainEvent(IDomainEvent @event)
            => _domainEvents.Add(@event);
    }
}
