using Common.Application.MediatR.Messaging;
using Common.Domain;
using Identity.Domain.Repository;
using Identity.Application.Menu.Queries;

namespace Identity.Application.Menu.Handlers;

public class GetMenuQueryHandler(IMenuRepository menuRepository)
    : IQueryHandler<GetMenuQuery, MenuDto>
{
    public async Task<Result<MenuDto>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var menu = await menuRepository.GetAsync(request.Id);
        if (menu is null)
        {
            return Result.Error<MenuDto>($"Menu with ID {request.Id} not found");
        }

        return new MenuDto(menu.Id, menu.MenuName, menu.Path, menu.Icon, menu.ParentId, menu.Sort);
    }
}
