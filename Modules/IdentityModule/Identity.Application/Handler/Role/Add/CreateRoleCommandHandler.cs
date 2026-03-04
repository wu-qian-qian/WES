using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Role.Commands;

public class CreateRoleCommandHandler(IRoleRepository _roleRepository, IUnitOfWork _unitOfWork)
: ICommandHandler<CreateRoleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Identity.Domain.Entities.Role
        {
            RoleName = request.RoleName,
            Description = request.Description
        };

        await _roleRepository.InserAsync(role);
        await _unitOfWork.SaveChangesAsync();
        return role.Id;
    }
}
