using Common.Application.MediatR.Messaging;

namespace Identity.Application.User.Queries;

public class GetUserListQuery : IQuery<List<UserDto>>
{
}
