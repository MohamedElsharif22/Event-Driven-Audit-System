namespace DH.EventDrivenAuditSystem.Application.DTOs
{
    public sealed record AuditLogResponse(
        int Id,
        int UserId,
        string Action,
        string EntityName,
        int EntityId,
        DateTime CreatedAt,
        string? Metadata
    );
}
