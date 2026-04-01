using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.UserRole.Queries;

namespace Identity.Application.UserRole.Handlers;

public class GetUserRoleQueryHandler(IUserRoleRepository userRoleRepository)
    : IQueryHandler<GetUserRoleQuery, UserRoleDto>
{
    public async Task<Result<UserRoleDto>> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
    {
        var userRole = await userRoleRepository.GetAsync(request.Id);
        if (userRole is null)
        {
            return Result.Error<UserRoleDto>($"UserRole with ID {request.Id} not found");
        }

        return new UserRoleDto(userRole.Id, userRole.UserId, userRole.RoleId);
    }
}
