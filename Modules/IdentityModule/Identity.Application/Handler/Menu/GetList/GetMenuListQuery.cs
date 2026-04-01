using Common.Application.MediatR.Messaging;

namespace Identity.Application.Menu.Queries;

public class GetMenuListQuery : IQuery<List<MenuDto>>
{
}
