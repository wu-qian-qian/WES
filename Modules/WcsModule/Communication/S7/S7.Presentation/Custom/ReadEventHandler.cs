using Common.Application.EventBus;
using Common.Domain;
using MediatR;
using NPOI.SS.Formula.Functions;
using S7.Application.Handlers;
using S7.CustomEvents;

namespace S7.Presentationl;

public class ReadEventHandler(ISender _sender)
: IEventHandler<ReadIntegrationEvent, Result<IEnumerable<ReadModel>>>
{
    public async Task<Result<IEnumerable<ReadModel>>> Handle(ReadIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<ReadModel>> result=default;
        var resultModel=await _sender.Send(new ReadBufferCommand{DeviceName=@event.DeviceName});
        List<ReadModel> readModels=new();
        if (resultModel.IsSuccess)
        {
            foreach(var item in resultModel.Value)
            {
                readModels.Add(new ReadModel(item.DBName,item.DBValue));
            }
        }
        if (readModels.Count > 0)
        {
           result= Result.Success<IEnumerable<ReadModel>>(readModels);
        }
        return result??Result.Error<IEnumerable<ReadModel>>($"输入读取失败,{resultModel.Message}");
    }
}