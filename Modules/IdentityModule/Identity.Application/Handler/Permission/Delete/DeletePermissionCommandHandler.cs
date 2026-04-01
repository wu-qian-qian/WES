using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Permission.Commands;

public class DeletePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeletePermissionCommand>
{
    public async Task<Result> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetAsync(request.Id);
        if (permission is null)
        {
            return Result.Error($"Permission with ID {request.Id} not found");
        }

        await permissionRepository.DeletesAsync(permission);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
