using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.User.Commands;

public class UpdateUserCommandHandler(IUserRepository _userRepository, IUnitOfWork _unitOfWork)
: ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Id);
        if (user is null)
        {
            return Result.Error($"User with ID {request.Id} not found");
        }

        if (!string.IsNullOrEmpty(request.Username))
        {
            user.Username = request.Username;
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            user.PasswordHash = request.Password;
        }

        if (!string.IsNullOrEmpty(request.Nickname))
        {
            user.Nickname = request.Nickname;
        }

        if (request.IsEnabled.HasValue)
        {
            user.IsEnabled = request.IsEnabled.Value;
        }

        await _userRepository.UpdatesAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
