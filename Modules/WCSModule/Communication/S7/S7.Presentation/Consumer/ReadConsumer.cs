using Common.Application.Caching;
using Common.Application.Log;
using Common.Domain;
using Common.Domain.Log;
using MassTransit;
using MediatR;
using S7.Application.Events.ReadBuffer;
namespace S7.Presentation;

public class ReadConsumer(ISender _sender,ILogService _logService,ICacheService _cacheService): IConsumer<ReadMessage>
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
                _logService.WriteErrorLog(LogCategoryType.Communication, $"设备号：{message.DeviceName} \t 读取失败: {result.Message}");
            }
        }
        catch (Exception ex)
        {
          _logService.WriteErrorLog(LogCategoryType.Communication, $"设备号：{message.DeviceName} \t 读取失败: {ex.Message}");
        }
    }
}
