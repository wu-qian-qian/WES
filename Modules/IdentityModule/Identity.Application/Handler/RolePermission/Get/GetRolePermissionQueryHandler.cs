using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.RolePermission.Queries;

namespace Identity.Application.RolePermission.Handlers;

public class GetRolePermissionQueryHandler(IRolePermissionRepository rolePermissionRepository)
    : IQueryHandler<GetRolePermissionQuery, RolePermissionDto>
{
    public async Task<Result<RolePermissionDto>> Handle(GetRolePermissionQuery request, CancellationToken cancellationToken)
    {
        var rolePermission = await rolePermissionRepository.GetAsync(request.Id);
        if (rolePermission is null)
        {
            return Result.Error<RolePermissionDto>($"RolePermission with ID {request.Id} not found");
        }

        return new RolePermissionDto(rolePermission.Id, rolePermission.RoleId, rolePermission.PermissionId);
    }
}
