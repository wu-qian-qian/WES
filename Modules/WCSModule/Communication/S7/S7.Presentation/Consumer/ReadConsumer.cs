using Common.Application.Log;
using Common.Domain;
using MassTransit;
using MediatR;
using S7.Application.Events.ReadBuffer;
namespace S7.Presentation;

public class ReadConsumer(ISender _sender,ILogService logService): IConsumer<ReadMessage>
{
    public async Task Consume(ConsumeContext<ReadMessage> context)
    {
        var message = context.Message;
        var command = new ReadBufferCommand
        {
            DeviceName = message.DeviceName,
            ReadKey = message.CacheKey.ToString(),
        };
        try
        {
            Result result= await _sender.Send(command);
            if (result.IsSuccess == false)
            {
                
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error processing ReadBufferCommand", ex);
        }
    }
}
