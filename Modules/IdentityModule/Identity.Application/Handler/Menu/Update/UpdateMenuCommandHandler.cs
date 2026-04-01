using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Menu.Commands;

public class UpdateMenuCommandHandler(
    IMenuRepository menuRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMenuCommand>
{
    public async Task<Result> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = await menuRepository.GetAsync(request.Id);
        if (menu is null)
        {
            return Result.Error($"Menu with ID {request.Id} not found");
        }

        if (!string.IsNullOrWhiteSpace(request.MenuName))
        {
            menu.MenuName = request.MenuName;
        }

        if (request.Path is not null)
        {
            menu.Path = request.Path;
        }

        if (request.Icon is not null)
        {
            menu.Icon = request.Icon;
        }

        if (request.ParentId != menu.ParentId)
        {
            menu.ParentId = request.ParentId;
        }

        if (request.Sort.HasValue)
        {
            menu.Sort = request.Sort.Value;
        }

        await menuRepository.UpdatesAsync(menu);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
