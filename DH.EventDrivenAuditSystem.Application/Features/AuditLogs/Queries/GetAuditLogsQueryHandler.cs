using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DH.EventDrivenAuditSystem.Application.Features.AuditLogs.Queries
{
    public sealed class GetAuditLogsQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAuditLogsQueryHandler> logger)
        : IRequestHandler<GetAuditLogsQuery, Result<IEnumerable<AuditLogResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<GetAuditLogsQueryHandler> _logger = logger;

        public async Task<Result<IEnumerable<AuditLogResponse>>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var auditLogs = _unitOfWork.Repository<AuditLog>().GetAllAsync()
                    .OrderByDescending(al => al.CreatedAt)
                    .ToList();

                if (!auditLogs.Any())
                {
                    _logger.LogInformation("No audit logs found.");
                    return Result<IEnumerable<AuditLogResponse>>.Failure("No audit logs found.");
                }

                var response = auditLogs.Select(al => new AuditLogResponse(
                    Id: al.Id,
                    UserId: al.UserId,
                    Action: al.Action,
                    EntityName: al.EntityName,
                    EntityId: al.EntityId,
                    CreatedAt: al.CreatedAt,
                    Metadata: al.Metadata
                )).ToList();

                _logger.LogInformation("Retrieved {Count} audit logs.", response.Count);

                return Result<IEnumerable<AuditLogResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving audit logs: {Message}", ex.Message);
                return Result<IEnumerable<AuditLogResponse>>.Failure($"An error occurred while retrieving audit logs: {ex.Message}");
            }
        }
    }
}
