using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.PermissionMenu.Queries;

namespace Identity.Application.PermissionMenu.Handlers;

public class GetPermissionMenuListQueryHandler(IPermissionMenuRepository permissionMenuRepository)
    : IQueryHandler<GetPermissionMenuListQuery, List<PermissionMenuDto>>
{
    public async Task<Result<List<PermissionMenuDto>>> Handle(GetPermissionMenuListQuery request, CancellationToken cancellationToken)
    {
        var queryable = await permissionMenuRepository.GetQueryableAsync();
        var permissionMenus = queryable
            .Select(x => new PermissionMenuDto(x.Id, x.PermissionId, x.MenuId))
            .ToList();

        return permissionMenus;
    }
}
