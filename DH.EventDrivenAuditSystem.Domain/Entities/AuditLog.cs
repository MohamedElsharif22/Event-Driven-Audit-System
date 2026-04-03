using DH.EventDrivenAuditSystem.Domain.Common;

namespace DH.EventDrivenAuditSystem.Domain.Entities
{
    public class AuditLog : BaseEntity
    {

        private AuditLog(int userId, string action, string entityName, int entityId, DateTime createdAt, string? metadata)
        {
            UserId = userId;
            Action = action;
            EntityName = entityName;
            EntityId = entityId;
            CreatedAt = createdAt;
            Metadata = metadata;
        }
        public int UserId { get; private set; }
        public string Action { get; private set; }
        public string EntityName { get; private set; }
        public int EntityId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? Metadata { get; private set; }
        public static AuditLog Create(int userId, string action, string entityName, int entityId, DateTime createdAt, string? metadata = null)
        {
            // Validate inputs
            if (userId <= 0)
                throw new ArgumentException("UserId must be greater than 0", nameof(userId));

            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentException("Action cannot be null or empty", nameof(action));

            if (string.IsNullOrWhiteSpace(entityName))
                throw new ArgumentException("EntityName cannot be null or empty", nameof(entityName));

            if (entityId <= 0)
                throw new ArgumentException("EntityId must be greater than 0", nameof(entityId));

            return new AuditLog(userId, action, entityName, entityId, createdAt, metadata);
        }
    }
}
