using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Application.User.Queries;
using Identity.Domain.Repository;

namespace Identity.Application.User.Handlers;

public class GetUserListQueryHandler(IUserRepository _userRepository)
: IQueryHandler<GetUserListQuery, List<UserDto>>
{
    public async Task<Result<List<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var queryable = await _userRepository.GetQueryableAsync();
        var users = queryable.ToList();

        var userDtos = users.Select(u => new UserDto(
            Id: u.Id,
            Username: u.Username,
            Nickname: u.Nickname,
            IsEnabled: u.IsEnabled
        )).ToList();

        return userDtos;
    }
}
