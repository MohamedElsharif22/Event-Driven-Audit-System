using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.Events;
using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DH.EventDrivenAuditSystem.Application.Features.Enrollments.Commands
{
    public class CreateEnrollmentCommandHandler(IUnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<CreateEnrollmentCommand> logger) : IRequestHandler<CreateEnrollmentCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<CreateEnrollmentCommand> _logger = logger;

        public async Task<Result<string>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
            if (user is null)
                return Result<string>.Failure("User not found.");

            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(request.CourseId);
            if (course is null)
                return Result<string>.Failure("Course not found.");

            var enrollment = course.AddEnrollment(user);
            int result;
            try
            {
                result = await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An error occurred while creating the enrollment: {ex.Message}");
            }

            if (result <= 0)
                return Result<string>.Failure("Failed to create enrollment.");

            var fetchedEnrollment = await _unitOfWork.Repository<Enrollment>().FirstOrDefaultAsync(e => e.UserId == request.UserId && e.CourseId == request.CourseId);
            if (fetchedEnrollment is null)
                return Result<string>.Failure("Enrollment created but could not be retrieved.");

            _logger.LogInformation("Enrollment created successfully for UserId: {UserId} and CourseId: {CourseId}", request.UserId, request.CourseId);

            await _mediator.Publish(
                new CourseEnrolledEvent(fetchedEnrollment.Id, fetchedEnrollment.UserId, fetchedEnrollment.CourseId, fetchedEnrollment.EnrollmentDate),
                cancellationToken
            );
            _logger.LogInformation("CourseEnrolledEvent published for EnrollmentId: {EnrollmentId}", fetchedEnrollment.Id);


            return Result<string>.Success("Enrollment created successfully.");
        }
    }
}
