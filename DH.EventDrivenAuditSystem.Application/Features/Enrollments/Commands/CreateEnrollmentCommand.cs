using DH.EventDrivenAuditSystem.Application.Common;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Commands
{
    public sealed record CreateEnrollmentCommand(int UserId, int CourseId) : IRequest<Result<string>>;
}

