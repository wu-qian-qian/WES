using Common.Application.MediatR.Messaging;
using Common.Domain;
using S7.Application.Abstractions.Data;
using S7.Domain.Repository;

namespace S7.Application.Handlers.GetPlcNet;

public class GetPlcNetCommandHandler(IPlcNetRepository _plcNetRepository)
    : ICommandHandler<GetPlcNetCommand, IEnumerable<S7NetModel>>
{
    public async Task<Result<IEnumerable<S7NetModel>>> Handle(GetPlcNetCommand request,
        CancellationToken cancellationToken)
    {
        IEnumerable<S7NetModel> plcNets = (await _plcNetRepository.GetQueryableAsync())
            .Where(x => x.IsUse)
            .Select(x => new S7NetModel
            {
                Ip = x.Ip,
                Rack = x.Rack,
                Solt = x.Solt,
                S7Type = x.S7Type,
                Port = x.Port,
                MaxRetries = x.MaxRetries,
                DelayMs = x.DelayMs,
                ReadTimeOut = x.ReadTimeOut,
                WriteTimeOut = x.WriteTimeOut,
                ReadHeart = x.ReadHeart,
                WriteHeart = x.WriteHeart
            }).ToArray();

        return Result.Success(plcNets);
    }
}