using DH.EventDrivenAuditSystem.Application.Features.Audit.Events;
using DH.EventDrivenAuditSystem.Domain.Entities;
using DH.EventDrivenAuditSystem.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DH.EventDrivenAuditSystem.Infrastructure.BackgroundServices
{
    /// <summary>
    /// Long-running background service.
    /// Reads AuditEvents from the channel and persists them to the database.
    /// Uses IServiceScopeFactory because DbContext is scoped, not singleton.
    /// </summary>
    public class AuditWorker(AuditChannel auditChannel,
        IServiceScopeFactory scopeFactory,
        ILogger<AuditWorker> logger) : BackgroundService
    {
        private readonly AuditChannel _auditChannel = auditChannel;
        private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
        private readonly ILogger<AuditWorker> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AuditWorker started. Listening for audit events...");

            await foreach (AuditEvent evt in _auditChannel.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    await PersistAuditLogAsync(evt, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to persist audit log for action {Action}", evt.Action);
                }
            }

            _logger.LogInformation("AuditWorker stopped.");
        }

        private async Task PersistAuditLogAsync(AuditEvent evt, CancellationToken cancellationToken)
        {
            // Create a fresh DI scope per audit event — DbContext is scoped, not singleton
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Use factory method to create immutable AuditLog with validation
            var log = AuditLog.Create(
                userId: evt.UserId,
                action: evt.Action,
                entityName: evt.EntityName,
                entityId: evt.EntityId,
                createdAt: evt.Timestamp,
                metadata: evt.Metadata
            );

            db.AuditLogs.Add(log);
            await db.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("AuditLog saved: {Action} on {Entity} ({EntityId} at {Timestamp})",
                evt.Action, evt.EntityName, evt.EntityId, DateTime.UtcNow);
        }
    }
}