using Common.Application.MediatR.Messaging;
using Common.Application.UnitOfWork;
using Common.Domain;
using Identity.Domain.Repository;

namespace Identity.Application.Menu.Commands;

public class DeleteMenuCommandHandler(
    IMenuRepository menuRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteMenuCommand>
{
    public async Task<Result> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = await menuRepository.GetAsync(request.Id);
        if (menu is null)
        {
            return Result.Error($"Menu with ID {request.Id} not found");
        }

        await menuRepository.DeletesAsync(menu);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
