using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.UserRole.Queries;

namespace Identity.Application.UserRole.Handlers;

public class GetUserRoleListQueryHandler(IUserRoleRepository userRoleRepository)
    : IQueryHandler<GetUserRoleListQuery, List<UserRoleDto>>
{
    public async Task<Result<List<UserRoleDto>>> Handle(GetUserRoleListQuery request, CancellationToken cancellationToken)
    {
        var queryable = await userRoleRepository.GetQueryableAsync();
        var userRoles = queryable
            .Select(x => new UserRoleDto(x.Id, x.UserId, x.RoleId))
            .ToList();

        return userRoles;
    }
}
