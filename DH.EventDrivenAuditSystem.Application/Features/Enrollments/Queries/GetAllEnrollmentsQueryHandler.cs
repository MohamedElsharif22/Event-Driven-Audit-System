using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Domain.Entities;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Queries
{
    public sealed class GetAllEnrollmentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEnrollmentsQuery, Result<IEnumerable<Enrollment>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IEnumerable<Enrollment>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _unitOfWork.Repository<Enrollment>().GetAllAsync();
            if (enrollments == null || !enrollments.Any())
                return Result<IEnumerable<Enrollment>>.Failure("No enrollments found.");

            return Result<IEnumerable<Enrollment>>.Success(enrollments);

        }
    }
}
