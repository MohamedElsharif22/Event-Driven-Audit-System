namespace DH.EventDrivenAuditSystem.Application.Features.Audit.Events;

/// <summary>
/// Integration event for audit trail logging.
/// Published when domain events occur to maintain an audit trail.
/// </summary>
public record AuditEvent(
    int UserId,
    string Action,
    string EntityName,
    int EntityId,
    DateTime Timestamp,
    string? Metadata = null
);
