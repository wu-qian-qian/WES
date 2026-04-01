using Common.Application.MediatR.Messaging;

namespace Identity.Application.RolePermission.Queries;

public class GetRolePermissionListQuery : IQuery<List<RolePermissionDto>>
{
}
