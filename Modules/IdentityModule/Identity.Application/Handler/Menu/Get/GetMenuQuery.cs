using Common.Application.MediatR.Messaging;

namespace Identity.Application.Menu.Queries;

public class GetMenuQuery : IQuery<MenuDto>
{
    public Guid Id { get; set; }
}
