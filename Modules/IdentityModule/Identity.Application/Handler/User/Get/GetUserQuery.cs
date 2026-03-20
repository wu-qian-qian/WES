using Common.Application.MediatR.Messaging;

namespace Identity.Application.User.Queries;

public class GetUserQuery : IQuery<UserDto>
{
    public Guid Id { get; set; }
}
