using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.PermissionMenu.Commands;

public class DeletePermissionMenuCommandHandler(
    IPermissionMenuRepository permissionMenuRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeletePermissionMenuCommand>
{
    public async Task<Result> Handle(DeletePermissionMenuCommand request, CancellationToken cancellationToken)
    {
        var permissionMenu = await permissionMenuRepository.GetAsync(request.Id);
        if (permissionMenu is null)
        {
            return Result.Error($"PermissionMenu with ID {request.Id} not found");
        }

        await permissionMenuRepository.DeletesAsync(permissionMenu);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
