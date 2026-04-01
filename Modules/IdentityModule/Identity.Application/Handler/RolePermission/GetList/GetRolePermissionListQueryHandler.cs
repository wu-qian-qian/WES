using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.RolePermission.Queries;

namespace Identity.Application.RolePermission.Handlers;

public class GetRolePermissionListQueryHandler(IRolePermissionRepository rolePermissionRepository)
    : IQueryHandler<GetRolePermissionListQuery, List<RolePermissionDto>>
{
    public async Task<Result<List<RolePermissionDto>>> Handle(GetRolePermissionListQuery request, CancellationToken cancellationToken)
    {
        var queryable = await rolePermissionRepository.GetQueryableAsync();
        var rolePermissions = queryable
            .Select(x => new RolePermissionDto(x.Id, x.RoleId, x.PermissionId))
            .ToList();

        return rolePermissions;
    }
}
