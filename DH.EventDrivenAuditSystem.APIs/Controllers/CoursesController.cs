using DH.EventDrivenAuditSystem.APIs.Responses;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Application.Features.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DH.EventDrivenAuditSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(ILogger<CoursesController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Retrieves all course records.
        /// </summary>
        /// <returns>An asynchronous operation that returns an <see cref="ActionResult{T}"/> containing an <see
        /// cref="ApiResponse{T}"/> with a collection of <see cref="CourseResponse"/> objects if the operation is
        /// successful; otherwise, an <see cref="ApiResponse{T}"/> with an error message.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CourseResponse>>>> GetAllCourses()
        {
            _logger.LogInformation("GetAllCourses endpoint called at {Timestamp}", DateTime.UtcNow);

            var result = await _mediator.Send(new GetAllCoursesQuery());

            return result.IsSuccess
                ? Ok(new ApiResponse<IEnumerable<CourseResponse>>(result.Value!))
                : BadRequest(new ApiResponse<string>(result.ErrorMessage ?? "Bad Request", false));
        }
    }
}
