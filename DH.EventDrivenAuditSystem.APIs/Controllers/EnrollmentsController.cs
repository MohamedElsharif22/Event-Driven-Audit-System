using DH.EventDrivenAuditSystem.APIs.Responses;
using DH.EventDrivenAuditSystem.Application.Features.Enrollments.Commands;
using DH.EventDrivenAuditSystem.Application.Features.Enrollments.Queries;
using DH.EventDrivenAuditSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DH.EventDrivenAuditSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController(ILogger<EnrollmentsController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<EnrollmentsController> _logger = logger;
        private readonly IMediator _mediator = mediator;


        /// <summary>
        /// Retrieves all enrollment records.
        /// </summary>
        /// <returns>An asynchronous operation that returns an <see cref="ActionResult{T}"/> containing an <see
        /// cref="ApiResponse{T}"/> with a collection of <see cref="Enrollment"/> objects if the operation is
        /// successful; otherwise, an <see cref="ApiResponse{T}"/> with an error message.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Enrollment>>>> GetEnrollments()
        {
            var result = await mediator.Send(new GetAllEnrollmentsQuery());

            return result.IsSuccess
                ? Ok(new ApiResponse<IEnumerable<Enrollment>>(result.Value!))
                : BadRequest(new ApiResponse<string>(result.ErrorMessage ?? "Bad Request", false));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> CreateEnrollment(CreateEnrollmentCommand request)
        {
            var command = new CreateEnrollmentCommand(request.UserId, request.CourseId);
            var result = await mediator.Send(command);

            _logger.LogInformation("CreateEnrollmentCommand executed with UserId: {UserId}, CourseId: {CourseId}, Result: {Result} at {DateTime.Now}",
                request.UserId, request.CourseId, result.IsSuccess ? "Success" : $"Failure - {result.ErrorMessage}", DateTime.Now);
            return result.IsSuccess
                ? Ok(new ApiResponse<string>(result.Value!))
                : BadRequest(new ApiResponse<string>(result.ErrorMessage ?? "Bad Request", false));
        }
    }
}
