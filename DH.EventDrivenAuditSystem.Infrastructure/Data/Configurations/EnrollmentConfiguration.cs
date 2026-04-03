using DH.EventDrivenAuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DH.EventDrivenAuditSystem.Infrastructure.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {



            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.CourseId)
                .IsRequired();

            builder.Property(e => e.EnrollmentDate)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
