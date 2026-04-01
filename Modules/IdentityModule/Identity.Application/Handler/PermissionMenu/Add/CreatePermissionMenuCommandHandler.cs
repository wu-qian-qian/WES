using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.PermissionMenu.Commands;

public class CreatePermissionMenuCommandHandler(
    IPermissionMenuRepository permissionMenuRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreatePermissionMenuCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreatePermissionMenuCommand request, CancellationToken cancellationToken)
    {
        var permissionMenu = new Identity.Domain.Entities.PermissionMenu
        {
            PermissionId = request.PermissionId,
            MenuId = request.MenuId
        };

        await permissionMenuRepository.InserAsync(permissionMenu);
        await unitOfWork.SaveChangesAsync();
        return permissionMenu.Id;
    }
}
