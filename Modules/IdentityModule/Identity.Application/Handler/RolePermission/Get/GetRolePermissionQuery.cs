using Common.Application.MediatR.Messaging;

namespace Identity.Application.RolePermission.Queries;

public class GetRolePermissionQuery : IQuery<RolePermissionDto>
{
    public Guid Id { get; set; }
}
