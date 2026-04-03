using DH.EventDrivenAuditSystem.Application.Events;
using DH.EventDrivenAuditSystem.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DH.EventDrivenAuditSystem.Application.Handlers;

/// <summary>
/// Reacts to CourseEnrolledEvent and pushes an AuditEvent into the
/// Channel<AuditEvent>. This handler returns immediately — the actual
/// DB write happens inside AuditWorker (BackgroundService).
/// </summary>
public class AuditNotificationHandler : INotificationHandler<CourseEnrolledEvent>
{
    private readonly IAuditQueue _auditQueue;
    private readonly ILogger<AuditNotificationHandler> _logger;

    public AuditNotificationHandler(IAuditQueue auditQueue, ILogger<AuditNotificationHandler> logger)
    {
        _auditQueue = auditQueue;
        _logger = logger;
    }

    public async Task Handle(CourseEnrolledEvent notification, CancellationToken cancellationToken)
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
        // Log the event for debugging purposes
        _logger.LogInformation("Enqueued audit event for user {UserId} enrolling in course {CourseId}", notification.UserId, notification.CourseId);
    }
}
