using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Courses.Queries
{
    public sealed record GetAllCoursesQuery() : IRequest<Result<IEnumerable<CourseResponse>>>;
}
