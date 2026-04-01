using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Permission.Commands;

public class CreatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreatePermissionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new Identity.Domain.Entities.Permission
        {
            PermissionCode = request.PermissionCode
        };

        await permissionRepository.InserAsync(permission);
        await unitOfWork.SaveChangesAsync();
        return permission.Id;
    }
}
