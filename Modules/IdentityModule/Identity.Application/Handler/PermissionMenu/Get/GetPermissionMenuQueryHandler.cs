using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.PermissionMenu.Queries;

namespace Identity.Application.PermissionMenu.Handlers;

public class GetPermissionMenuQueryHandler(IPermissionMenuRepository permissionMenuRepository)
    : IQueryHandler<GetPermissionMenuQuery, PermissionMenuDto>
{
    public async Task<Result<PermissionMenuDto>> Handle(GetPermissionMenuQuery request, CancellationToken cancellationToken)
    {
        var permissionMenu = await permissionMenuRepository.GetAsync(request.Id);
        if (permissionMenu is null)
        {
            return Result.Error<PermissionMenuDto>($"PermissionMenu with ID {request.Id} not found");
        }

        return new PermissionMenuDto(permissionMenu.Id, permissionMenu.PermissionId, permissionMenu.MenuId);
    }
}
