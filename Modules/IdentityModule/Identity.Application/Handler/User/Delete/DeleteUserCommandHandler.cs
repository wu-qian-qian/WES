using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.User.Commands;

public class DeleteUserCommandHandler(IUserRepository _userRepository, IUnitOfWork _unitOfWork)
: ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Id);
        if (user is null)
        {
            return Result.Error($"User with ID {request.Id} not found");
        }

        await _userRepository.DeletesAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
