using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Events
{
    public record CourseEnrolledEvent(
        int EnrollmentId,
        int UserId,
        int CourseId,
        DateTime EnrolledAt
    ) : INotification;
}
