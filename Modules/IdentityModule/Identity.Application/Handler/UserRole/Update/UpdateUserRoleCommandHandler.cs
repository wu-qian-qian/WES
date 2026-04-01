using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.UserRole.Commands;

public class UpdateUserRoleCommandHandler(
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateUserRoleCommand>
{
    public async Task<Result> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = await userRoleRepository.GetAsync(request.Id);
        if (userRole is null)
        {
            return Result.Error($"UserRole with ID {request.Id} not found");
        }

        userRole.UserId = request.UserId;
        userRole.RoleId = request.RoleId;

        await userRoleRepository.UpdatesAsync(userRole);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
