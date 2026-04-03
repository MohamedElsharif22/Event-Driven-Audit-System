using DH.EventDrivenAuditSystem.Domain.Common;

namespace DH.EventDrivenAuditSystem.Domain.Entities
{
    public class Course : BaseEntity
    {
        public Course(string title)
        {
            Title = title;
        }
        private List<Enrollment> _enrollments = new();
        public string Title { get; private set; }
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;

        public Enrollment AddEnrollment(User user)
        {
            var enrollment = new Enrollment(user.Id, Id);
            _enrollments.Add(enrollment);
            return enrollment;
        }
    }
}
