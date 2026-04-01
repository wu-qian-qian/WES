using Common.Application.MediatR.Messaging;

namespace Identity.Application.Permission.Queries;

public class GetPermissionListQuery : IQuery<List<PermissionDto>>
{
}
