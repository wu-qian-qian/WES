using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.RolePermission.Commands;

public class DeleteRolePermissionCommandHandler(
    IRolePermissionRepository rolePermissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteRolePermissionCommand>
{
    public async Task<Result> Handle(DeleteRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var rolePermission = await rolePermissionRepository.GetAsync(request.Id);
        if (rolePermission is null)
        {
            return Result.Error($"RolePermission with ID {request.Id} not found");
        }

        await rolePermissionRepository.DeletesAsync(rolePermission);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
