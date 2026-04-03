using DH.EventDrivenAuditSystem.Domain.Common;

namespace DH.EventDrivenAuditSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;
    }
}
