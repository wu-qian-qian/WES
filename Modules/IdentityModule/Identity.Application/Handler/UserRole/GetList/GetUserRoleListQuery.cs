using Common.Application.MediatR.Messaging;

namespace Identity.Application.UserRole.Queries;

public class GetUserRoleListQuery : IQuery<List<UserRoleDto>>
{
}
