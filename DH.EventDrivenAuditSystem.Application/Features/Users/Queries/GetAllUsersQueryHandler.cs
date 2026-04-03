using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using DH.EventDrivenAuditSystem.Domain.Common;
using DH.EventDrivenAuditSystem.Domain.Entities;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Users.Queries
{
    public sealed class GetAllUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _unitOfWork.Repository<User>()
                                   .GetAllAsync()
                                   .Select(u => new UserResponse(u.Id, u.Name))
                                   .ToList();

            if (users == null || !users.Any())
                return Result<IEnumerable<UserResponse>>.Failure("No users found.");

            return Result<IEnumerable<UserResponse>>.Success(users);
        }
    }
}
