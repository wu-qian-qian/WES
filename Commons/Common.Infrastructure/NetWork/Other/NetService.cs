using System.Collections.Concurrent;
using Common.Application.NetWork.Other.Base;
using Common.Application.NetWork.Other.Config;
using Common.Domain;

namespace Common.Infrastructure.NetWork.Other;

public class NetService : INetService
{
    public NetService()
    {
        NetMap = new ConcurrentDictionary<string, INet>();
    }

    public ConcurrentDictionary<string, INet> NetMap { get; }

    public async Task<Result<byte[]>> ReadAsync(IReadConfig input)
    {
        Result<byte[]>? result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
            if (net.IsConnect)
                result = await net.ReadAsync(input);
        return result ?? Result.Error<byte[]>("No connected net found");
    }

    public async Task<Result> WriteAsync(IWriteConfig input)
    {
        Result? result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
            if (net.IsConnect)
                result = await net.WriteAsync(input);
        return result ?? new Result(false, "No connected net found");
    }

    public async Task<Result> ReConnectAsync(INetConfig input)
    {
        Result? result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
            if (!net.IsConnect)
                result = await net.ReConnectAsync();
        return result ?? new Result(false, "No connected net found");
    }

    public async Task<Result> AddConnectAsync(INet input)
    {
        var result = await input.ConnectAsync();
        if (result.IsSuccess) NetMap.AddOrUpdate(input._netConfig.Ip, input,(key,value)=>value=input);
        return result;
    }
}