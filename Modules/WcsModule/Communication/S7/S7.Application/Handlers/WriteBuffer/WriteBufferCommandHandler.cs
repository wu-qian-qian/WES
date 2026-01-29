using Common.Application.MediatR.Messaging;
using Common.Application.NetWork.Other.Base;
using Common.Domain;
using S7.Application.Services;

namespace S7.Application.Handlers.WriteBuffer;

public class WriteBufferCommandHandler (INetService _netService
,IWriteModelBuildService _modelService): ICommandHandler<WriteBufferCommand>
{
    public async Task<Result> Handle(WriteBufferCommand request, CancellationToken cancellationToken)
    {
        Result? result=default;
        var writeModels=await _modelService.PlcWriteModelBuildAsync(request.DeviceName,request.DBNameToDataValue);
        foreach(var item in writeModels)
        {
            result=await _netService.WriteAsync(item);
            if (result.IsSuccess == false)
            {
                result=Result.Error($"数据写入失败--{result.Message}");
                break;
            }
        }
        return result??Result.Error($"数据写入失败");
    }
}