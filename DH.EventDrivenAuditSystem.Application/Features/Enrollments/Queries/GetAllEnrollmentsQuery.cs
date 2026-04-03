using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Domain.Entities;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Queries
{
    public sealed record GetAllEnrollmentsQuery() : IRequest<Result<IEnumerable<EnrollmentResponse>>>;

}
