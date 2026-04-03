using DH.EventDrivenAuditSystem.Application.Features.Audit.Events;
using DH.EventDrivenAuditSystem.Application.Interfaces;
using System.Threading.Channels;


namespace DH.EventDrivenAuditSystem.Infrastructure.BackgroundServices
{
    /// <summary>
    /// Thread-safe unbounded in-memory queue.
    /// Registered as a singleton — shared between the API (Writer) and AuditWorker (Reader).
    /// </summary>
    public class AuditChannel : IAuditQueue
    {
        private readonly Channel<AuditEvent> _channel =
            Channel.CreateUnbounded<AuditEvent>(new UnboundedChannelOptions
            {
                SingleReader = true   // only AuditWorker reads
            });

        public ChannelReader<AuditEvent> Reader => _channel.Reader;

        public ValueTask EnqueueAsync(AuditEvent auditEvent, CancellationToken cancellationToken = default)
            => _channel.Writer.WriteAsync(auditEvent, cancellationToken);
    }

}