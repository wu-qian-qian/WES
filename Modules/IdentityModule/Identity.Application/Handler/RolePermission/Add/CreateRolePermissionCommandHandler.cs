using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.RolePermission.Commands;

public class CreateRolePermissionCommandHandler(
    IRolePermissionRepository rolePermissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateRolePermissionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var rolePermission = new Identity.Domain.Entities.RolePermission
        {
            RoleId = request.RoleId,
            PermissionId = request.PermissionId
        };

        await rolePermissionRepository.InserAsync(rolePermission);
        await unitOfWork.SaveChangesAsync();
        return rolePermission.Id;
    }
}
