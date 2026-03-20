using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Role.Commands;

public class DeleteRoleCommandHandler(IRoleRepository _roleRepository, IUnitOfWork _unitOfWork)
: ICommandHandler<DeleteRoleCommand>
{
    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetAsync(request.Id);
        if (role is null)
        {
            return Result.Error($"Role with ID {request.Id} not found");
        }

        await _roleRepository.DeletesAsync(role);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
