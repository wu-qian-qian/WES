using Common.Application.EventBus;
using Common.Domain;
using MediatR;
using S7.Application.Handlers;
using S7.Application.Handlers.WriteBuffer;
using S7.CustomEvents;

namespace S7.Presentationl;

public class WriteEventHandler(ISender _sender)
: IEventHandler<WriteIntegrationEvent, Result>
{
    public async Task<Result> Handle(WriteIntegrationEvent @event
    , CancellationToken cancellationToken = default)
    {
        return await _sender.Send(new WriteBufferCommand
        {
            DeviceName=@event.DeviceName,
            DBNameToDataValue=@event.DBNameToDataValue
        });
    }
}