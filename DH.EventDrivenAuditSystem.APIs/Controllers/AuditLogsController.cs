using DH.EventDrivenAuditSystem.APIs.Responses;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Application.Features.AuditLogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DH.EventDrivenAuditSystem.APIs.Controllers
{
    /// <summary>
    /// Controller for managing audit log retrieval.
    /// Provides endpoints to view immutable audit trail records.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController(ILogger<AuditLogsController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<AuditLogsController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves all audit logs ordered by creation date (newest first).
        /// </summary>
        /// <returns>
        /// Returns 200 OK with a collection of all audit logs if successful.
        /// Returns 400 Bad Request if no audit logs exist.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<AuditLogResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<IEnumerable<AuditLogResponse>>>> GetAuditLogs()
        {
            _logger.LogInformation("GetAuditLogs called");

            var query = new GetAuditLogsQuery();
            var result = await _mediator.Send(query);

            return result.IsSuccess
                ? Ok(new ApiResponse<IEnumerable<AuditLogResponse>>(result.Value!))
                : BadRequest(new ApiResponse<string>(
                    result.ErrorMessage ?? "No audit logs found.",
                    false));
        }

    }
}
