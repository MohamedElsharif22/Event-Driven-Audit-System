using DH.EventDrivenAuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DH.EventDrivenAuditSystem.Infrastructure.Data.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {

            builder.Property(a => a.UserId)
                .IsRequired();

            builder.Property(a => a.Action)
                .IsRequired();

            builder.Property(a => a.EntityName)
                .IsRequired();

            builder.Property(a => a.EntityId)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.Metadata);

        }
    }
}
