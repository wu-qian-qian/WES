using Common.Application.MediatR.Messaging;

namespace Identity.Application.PermissionMenu.Queries;

public class GetPermissionMenuListQuery : IQuery<List<PermissionMenuDto>>
{
}
