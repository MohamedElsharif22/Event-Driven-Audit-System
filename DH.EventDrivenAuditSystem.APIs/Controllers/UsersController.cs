using DH.EventDrivenAuditSystem.APIs.Responses;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DH.EventDrivenAuditSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ILogger<UsersController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<UsersController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves all user records.
        /// </summary>
        /// <returns>An asynchronous operation that returns an <see cref="ActionResult{T}"/> containing an <see
        /// cref="ApiResponse{T}"/> with a collection of <see cref="UserResponse"/> objects if the operation is
        /// successful; otherwise, an <see cref="ApiResponse{T}"/> with an error message.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponse>>>> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers endpoint called at {Timestamp}", DateTime.UtcNow);

            var result = await _mediator.Send(new GetAllUsersQuery());

            return result.IsSuccess
                ? Ok(new ApiResponse<IEnumerable<UserResponse>>(result.Value!))
                : BadRequest(new ApiResponse<string>(result.ErrorMessage ?? "Bad Request", false));
        }
    }
}
