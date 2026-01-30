using Common.Application.MediatR.Messaging;
using Common.Application.NetWork.Other.Base;
using Common.Domain;
using S7.Application.Abstractions;
using S7.Application.Abstractions.Data;
using S7.Application.Services;
using S7.Domain.Repository;

namespace S7.Application.Handlers.UpdateCache;

public class UpdateCacheCommandHandler (IPlcEntityRepository _plcEntityRepository
,INetService _netService,IPlcNetRepository _plcNetRepository,IWriteModelBuildService _writeModelService
        ,IReadModelBuildService _readModelService,IS7NetFactory _netFactory): ICommandHandler<UpdateCacheCommand>
{
    public async Task<Result> Handle(UpdateCacheCommand request, CancellationToken cancellationToken)
    {
        try
        {
        if (request.LoadNet)
            {
            var nets=(await _plcNetRepository.GetQueryableAsync())
            .Where(p=>p.IsUse).ToArray();
            for(int i = 0; i < nets.Length; i++)
            {
                S7NetModel s7Net=new S7NetModel
                {
                   DelayMs=nets[i].DelayMs,  
                   Ip=nets[i].Ip,
                    MaxRetries=nets[i].MaxRetries,
                    Port=nets[i].Port,
                    Rack=nets[i].Rack,
                    ReadHeart=nets[i].ReadHeart,
                    ReadTimeOut=nets[i].ReadTimeOut,
                    S7Type=nets[i].S7Type,
                    Solt=nets[i].Solt,
                    WriteHeart=nets[i].WriteHeart,
                    WriteTimeOut=nets[i].WriteTimeOut,
                };
               await _netService.AddConnectAsync(_netFactory.CraetNet(s7Net));
            }
        }
        var plcEntitys=(await _plcEntityRepository.GetQueryableAsync())
        .Where(p=>p.IsUse);
        await _readModelService.LoadAsync(plcEntitys);
        await _writeModelService.LoadAsync(plcEntitys.Where(p=>p.IsWrite));
        return Result.Success(true);
        }
        catch(Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}