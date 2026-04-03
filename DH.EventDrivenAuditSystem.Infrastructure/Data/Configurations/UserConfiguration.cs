using DH.EventDrivenAuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DH.EventDrivenAuditSystem.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new User { Id = 1, Name = "Muhammad Kamal" },
                new User { Id = 2, Name = "Ali Zein" },
                new User { Id = 3, Name = "Khaled" }
            );

        }
    }
}
