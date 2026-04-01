using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.UserRole.Commands;

public class DeleteUserRoleCommandHandler(
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserRoleCommand>
{
    public async Task<Result> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = await userRoleRepository.GetAsync(request.Id);
        if (userRole is null)
        {
            return Result.Error($"UserRole with ID {request.Id} not found");
        }

        await userRoleRepository.DeletesAsync(userRole);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
