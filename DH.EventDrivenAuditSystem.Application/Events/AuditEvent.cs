namespace DH.EventDrivenAuditSystem.Application.Events
{
    public record AuditEvent(
        int UserId,
        string Action,
        string EntityName,
        int EntityId,
        DateTime Timestamp,
        string? Metadata = null
    );
}

