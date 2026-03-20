using Common.Application.MediatR.Messaging;

namespace Identity.Application.Role.Queries;

public class GetRoleListQuery : IQuery<List<RoleDto>>
{
}
