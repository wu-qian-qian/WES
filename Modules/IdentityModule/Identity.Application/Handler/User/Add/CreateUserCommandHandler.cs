using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.User.Commands;

public class CreateUserCommandHandler(IUserRepository _userRepository, IUnitOfWork _unitOfWork)
: ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
         var user = new Identity.Domain.Entities.User
        {
            Username = request.Username,
            PasswordHash = request.Password, // 这里应该使用密码哈希，暂时简化处理
            Nickname = request.Nickname,
            IsEnabled = request.IsEnabled
        };

        await _userRepository.InserAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return user.Id;
    }
}
