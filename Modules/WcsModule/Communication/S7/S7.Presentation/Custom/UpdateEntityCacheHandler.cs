using Common.Application.EventBus;
using Common.Domain;
using MediatR;
using S7.Application.Handlers.UpdateCache;
using S7.CustomEvents;

namespace S7.Presentationl;

public class UpdateEntityCacheHandler(ISender _sender)
: IIntegrationEventHandler<UpdateEntityCacheEvent, Result>
{
    public async Task<Result> Handle(UpdateEntityCacheEvent @event
    , CancellationToken cancellationToken = default)
    {
       return await  _sender.Send(new UpdateCacheCommand{LoadNet=@event.LoadNe});
    }
}