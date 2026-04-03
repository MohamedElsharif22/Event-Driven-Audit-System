using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.AuditLogs.Queries
{
    public sealed record GetAuditLogsQuery() : IRequest<Result<IEnumerable<AuditLogResponse>>>;
}
