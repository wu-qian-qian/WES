using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.PermissionMenu.Commands;

public class UpdatePermissionMenuCommandHandler(
    IPermissionMenuRepository permissionMenuRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdatePermissionMenuCommand>
{
    public async Task<Result> Handle(UpdatePermissionMenuCommand request, CancellationToken cancellationToken)
    {
        var permissionMenu = await permissionMenuRepository.GetAsync(request.Id);
        if (permissionMenu is null)
        {
            return Result.Error($"PermissionMenu with ID {request.Id} not found");
        }

        permissionMenu.PermissionId = request.PermissionId;
        permissionMenu.MenuId = request.MenuId;

        await permissionMenuRepository.UpdatesAsync(permissionMenu);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
