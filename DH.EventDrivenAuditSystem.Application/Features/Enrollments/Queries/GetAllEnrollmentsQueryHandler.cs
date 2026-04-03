using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Domain.Entities;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Queries
{
    public sealed class GetAllEnrollmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEnrollmentsQuery, Result<IEnumerable<EnrollmentResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IEnumerable<EnrollmentResponse>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments =  _unitOfWork.Repository<Enrollment>()
                                               .GetAllAsync()
                                               .Select(e => new EnrollmentResponse(e.CourseId,
                                                    e.Course.Title,
                                                    e.UserId,
                                                    e.User.Name,
                                                    e.EnrollmentDate,
                                                    e.ExpirationDate)).ToListAsync();

            if (enrollments == null || !enrollments.Any())
                return Result<IEnumerable<EnrollmentResponse>>.Failure("No enrollments found.");

            return Result<IEnumerable<EnrollmentResponse>>.Success(enrollments);

        }
    }
}
