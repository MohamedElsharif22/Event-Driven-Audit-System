using DH.EventDrivenAuditSystem.Domain.Common;

namespace DH.EventDrivenAuditSystem.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public Enrollment(int userId, int courseId)
        {
            UserId = userId;
            CourseId = courseId;
            EnrollmentDate = DateTime.UtcNow;
        }
        public int UserId { get; private set; }
        public int CourseId { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public User User { get; private set; }
        public Course Course { get; private set; }
    }
}
