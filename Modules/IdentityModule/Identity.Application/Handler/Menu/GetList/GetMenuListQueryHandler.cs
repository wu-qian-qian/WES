using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.Menu.Queries;

namespace Identity.Application.Menu.Handlers;

public class GetMenuListQueryHandler(IMenuRepository menuRepository)
    : IQueryHandler<GetMenuListQuery, List<MenuDto>>
{
    public async Task<Result<List<MenuDto>>> Handle(GetMenuListQuery request, CancellationToken cancellationToken)
    {
        var queryable = await menuRepository.GetQueryableAsync();
        var menus = queryable
            .OrderBy(x => x.Sort)
            .Select(x => new MenuDto(x.Id, x.MenuName, x.Path, x.Icon, x.ParentId, x.Sort))
            .ToList();

        return menus;
    }
}
