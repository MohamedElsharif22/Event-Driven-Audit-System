using DH.EventDrivenAuditSystem.Domain.Common;

namespace DH.EventDrivenAuditSystem.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public int UserId { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Metadata { get; set; }
    }
}
