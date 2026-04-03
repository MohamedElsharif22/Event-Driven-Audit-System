using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Events;

/// <summary>
/// Integration event published after a user successfully enrolls in a course.
/// This triggers side effects like logging, audit trail, notifications, etc.
/// </summary>
public sealed record CourseEnrolledNotification(
    int EnrollmentId,
    int UserId,
    int CourseId,
    DateTime EnrolledAt
) : INotification;
