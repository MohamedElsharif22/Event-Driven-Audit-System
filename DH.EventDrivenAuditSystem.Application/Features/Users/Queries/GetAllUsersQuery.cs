using DH.EventDrivenAuditSystem.Application.Common;
using DH.EventDrivenAuditSystem.Application.DTOs;
using MediatR;

namespace DH.EventDrivenAuditSystem.Application.Features.Users.Queries
{
    public sealed record GetAllUsersQuery() : IRequest<Result<IEnumerable<UserResponse>>>;
}
