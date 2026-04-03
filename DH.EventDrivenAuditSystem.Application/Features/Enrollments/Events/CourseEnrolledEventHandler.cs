using DH.EventDrivenAuditSystem.Application.Features.Audit.Events;
using DH.EventDrivenAuditSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Events;

/// <summary>
/// Handles the CourseEnrolledNotification event.
/// Reacts to user enrollment and creates an audit event in the audit queue.
/// The actual DB write happens inside AuditWorker (BackgroundService).
/// </summary>
public class CourseEnrolledEventHandler : INotificationHandler<CourseEnrolledNotification>
{
    private readonly IAuditQueue _auditQueue;
    private readonly ILogger<CourseEnrolledEventHandler> _logger;

    public CourseEnrolledEventHandler(IAuditQueue auditQueue, ILogger<CourseEnrolledEventHandler> logger)
    {
        _auditQueue = auditQueue;
        _logger = logger;
    }

    public async Task Handle(CourseEnrolledNotification notification, CancellationToken cancellationToken)
    {
        var auditEvent = new AuditEvent(
            UserId: notification.UserId,
            Action: "EnrollCourse",
            EntityName: "Enrollment",
            EntityId: notification.EnrollmentId,
            Timestamp: notification.EnrolledAt,
            Metadata: $"{{\"courseId\":\"{notification.CourseId}\"}}"
        );

        await _auditQueue.EnqueueAsync(auditEvent, cancellationToken);
        
        _logger.LogInformation(
            "Enqueued audit event for user {UserId} enrolling in course {CourseId}", 
            notification.UserId, 
            notification.CourseId);
    }
}
