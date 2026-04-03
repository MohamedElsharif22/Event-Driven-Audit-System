using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Domain.Courses;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Courses.Queries
{
    public sealed class GetAllCoursesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCoursesQuery, Result<IEnumerable<CourseResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = _unitOfWork.Repository<Course>()
                                     .GetAllAsync()
                                     .Select(c => new CourseResponse(c.Id, c.Title))
                                     .ToList();

            if (courses == null || !courses.Any())
                return Result<IEnumerable<CourseResponse>>.Failure("No courses found.");

            return Result<IEnumerable<CourseResponse>>.Success(courses);
        }
    }
}
