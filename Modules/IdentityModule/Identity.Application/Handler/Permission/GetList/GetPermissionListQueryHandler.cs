using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.Permission.Queries;

namespace Identity.Application.Permission.Handlers;

public class GetPermissionListQueryHandler(IPermissionRepository permissionRepository)
    : IQueryHandler<GetPermissionListQuery, List<PermissionDto>>
{
    public async Task<Result<List<PermissionDto>>> Handle(GetPermissionListQuery request, CancellationToken cancellationToken)
    {
        var queryable = await permissionRepository.GetQueryableAsync();
        var permissions = queryable
            .Select(x => new PermissionDto(x.Id, x.PermissionCode))
            .ToList();

        return permissions;
    }
}
