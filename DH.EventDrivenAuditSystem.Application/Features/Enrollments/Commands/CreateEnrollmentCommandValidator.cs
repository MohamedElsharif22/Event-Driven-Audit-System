using FluentValidation;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Commands
{
    public sealed class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
    {
        public CreateEnrollmentCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.CourseId)
                .NotEmpty()
                .GreaterThan(0).WithMessage("CourseId must be greater than 0.");
        }
    }
}
