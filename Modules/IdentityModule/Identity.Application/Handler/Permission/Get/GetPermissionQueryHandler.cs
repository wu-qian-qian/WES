using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.Permission.Queries;

namespace Identity.Application.Permission.Handlers;

public class GetPermissionQueryHandler(IPermissionRepository permissionRepository)
    : IQueryHandler<GetPermissionQuery, PermissionDto>
{
    public async Task<Result<PermissionDto>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetAsync(request.Id);
        if (permission is null)
        {
            return Result.Error<PermissionDto>($"Permission with ID {request.Id} not found");
        }

        return new PermissionDto(permission.Id, permission.PermissionCode);
    }
}
