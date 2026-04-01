using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Menu.Commands;

public class CreateMenuCommandHandler(
    IMenuRepository menuRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateMenuCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = new Identity.Domain.Entities.Menu
        {
            MenuName = request.MenuName,
            Path = request.Path ?? string.Empty,
            Icon = request.Icon ?? string.Empty,
            ParentId = request.ParentId,
            Sort = request.Sort
        };

        await menuRepository.InserAsync(menu);
        await unitOfWork.SaveChangesAsync();
        return menu.Id;
    }
}
