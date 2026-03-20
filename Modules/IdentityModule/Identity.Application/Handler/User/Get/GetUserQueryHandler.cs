using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Application.User.Queries;
using Identity.Domain.Repository;

namespace Identity.Application.User.Handlers;

public class GetUserQueryHandler(IUserRepository _userRepository)
: IQueryHandler<GetUserQuery, UserDto>
{
    public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Id);
        if (user is null)
        {
            return Result.Error<UserDto>($"User with ID {request.Id} not found");
        }

        return new UserDto(
            Id: user.Id,
            Username: user.Username,
            Nickname: user.Nickname,
            IsEnabled: user.IsEnabled
        );
    }
}
