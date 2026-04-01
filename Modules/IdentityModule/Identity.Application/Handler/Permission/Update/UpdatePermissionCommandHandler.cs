using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Permission.Commands;

public class UpdatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdatePermissionCommand>
{
    public async Task<Result> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetAsync(request.Id);
        if (permission is null)
        {
            return Result.Error($"Permission with ID {request.Id} not found");
        }

        if (!string.IsNullOrWhiteSpace(request.PermissionCode))
        {
            permission.PermissionCode = request.PermissionCode;
        }

        await permissionRepository.UpdatesAsync(permission);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
