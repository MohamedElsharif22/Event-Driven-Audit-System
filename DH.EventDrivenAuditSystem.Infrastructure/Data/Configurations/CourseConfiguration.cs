using DH.EventDrivenAuditSystem.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DH.EventDrivenAuditSystem.Infrastructure.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);


            builder.HasData(
                new Course("Introduction to C#") { Id = 1 },
                new Course("Entity Framework Core") { Id = 2 },
                new Course("ASP.NET Core Web API") { Id = 3 }
            );
        }
    }
}
