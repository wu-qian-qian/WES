using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Role.Commands;

public class UpdateRoleCommandHandler(IRoleRepository _roleRepository, IUnitOfWork _unitOfWork)
: ICommandHandler<UpdateRoleCommand>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(request.Id);
        if (role is null)
        {
            return Result.Error($"Role with ID {request.Id} not found");
        }

        if (!string.IsNullOrEmpty(request.RoleName))
        {
            role.RoleName = request.RoleName;
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            role.Description = request.Description;
        }

        await _roleRepository.UpdatesAsync(role);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
