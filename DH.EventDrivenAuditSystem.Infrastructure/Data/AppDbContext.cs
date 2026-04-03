using DH.EventDrivenAuditSystem.Domain.Courses;
using DH.EventDrivenAuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DH.EventDrivenAuditSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
