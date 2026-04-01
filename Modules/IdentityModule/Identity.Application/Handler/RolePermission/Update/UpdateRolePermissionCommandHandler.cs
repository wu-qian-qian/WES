using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.RolePermission.Commands;

public class UpdateRolePermissionCommandHandler(
    IRolePermissionRepository rolePermissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateRolePermissionCommand>
{
    public async Task<Result> Handle(UpdateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var rolePermission = await rolePermissionRepository.GetAsync(request.Id);
        if (rolePermission is null)
        {
            return Result.Error($"RolePermission with ID {request.Id} not found");
        }

        rolePermission.RoleId = request.RoleId;
        rolePermission.PermissionId = request.PermissionId;

        await rolePermissionRepository.UpdatesAsync(rolePermission);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
