using DH.EventDrivenAuditSystem.Application.Features.Audit.Events;

namespace DH.EventDrivenAuditSystem.Application.Interfaces
{
    public interface IAuditQueue
    {
        ValueTask EnqueueAsync(AuditEvent auditEvent, CancellationToken cancellationToken = default);
    }
}
