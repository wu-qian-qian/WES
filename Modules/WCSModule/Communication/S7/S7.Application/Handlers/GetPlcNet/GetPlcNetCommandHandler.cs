using Common.Application.MediatR.Messaging;
using Common.Domain;
using S7.Application.Abstractions.Data;
using S7.Domain;

public class GetPlcNetCommandHandler(IPlcNetRepository _plcNetRepository): ICommandHandler<GetPlcNetCommand, IEnumerable<NetModel>>
{
    public async Task<Result<IEnumerable<NetModel>>> Handle(GetPlcNetCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<NetModel> plcNets =(await _plcNetRepository.GetQueryableAsync())
            .Where(x => x.IsUse)
            .Select(x => new NetModel
            {
              Ip=x.Ip,
              Rack=x.Rack,
              Solt=x.Solt,
              S7Type=x.S7Type,
              Port=x.Port,
              MaxRetries=x.MaxRetries,
              DelayMs=x.DelayMs,
              ReadTimeOut=x.ReadTimeOut,
              WriteTimeOut=x.WriteTimeOut,
              ReadHeart=x.ReadHeart,
              WriteHeart=x.WriteHeart
            }).ToArray();

        return Result.Success(plcNets);
    }
}