using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.UserRole.Commands;

public class CreateUserRoleCommandHandler(
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateUserRoleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = new Identity.Domain.Entities.UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        };

        await userRoleRepository.InserAsync(userRole);
        await unitOfWork.SaveChangesAsync();
        return userRole.Id;
    }
}
