using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Domain.Courses.Events;
using DH.EventDrivenAuditSystem.Domain.Entities;

namespace DH.EventDrivenAuditSystem.Domain.Courses
{
    public class Enrollment : BaseEntity
    {
        public Enrollment(int userId, int courseId)
        {
            UserId = userId;
            CourseId = courseId;
            EnrollmentDate = DateTime.UtcNow;
            ExpirationDate = DateTime.UtcNow.AddMonths(1);

            // Raise domain event when enrollment is created
            RaiseDomainEvent(new CourseEnrolledEvent(
                UserId: userId,
                CourseId: courseId,
                EnrolledAt: EnrollmentDate
            ));
        }
        public int UserId { get; private set; }
        public int CourseId { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public User User { get; private set; }
        public Course Course { get; private set; }
    }
}
