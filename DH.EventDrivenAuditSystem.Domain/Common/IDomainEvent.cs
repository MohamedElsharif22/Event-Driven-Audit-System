namespace DH.EventDrivenAuditSystem.Domain.Common;

/// <summary>
/// Marker interface for domain events.
/// Domain events represent something that happened in the domain —
/// they are core to business logic and should be persisted as part of the audit trail.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the timestamp when this event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}
