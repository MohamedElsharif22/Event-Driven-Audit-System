using DH.EventDrivenAuditSystem.Domain.Common;

namespace DH.EventDrivenAuditSystem.Domain.Courses.Events;

/// <summary>
/// Domain event raised when a user enrolls in a course.
/// This represents a core business concept and is part of the domain model.
/// </summary>
public sealed record CourseEnrolledEvent(
    int UserId,
    int CourseId,
    DateTime EnrolledAt
) : IDomainEvent
{
    /// <summary>
    /// Gets the timestamp when this event occurred.
    /// </summary>
    public DateTime OccurredOn { get; } = EnrolledAt;
}
