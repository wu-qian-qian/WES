using Common.Application.NET.Other.Base;
using Common.Application.NET.Other.Config;
using Common.Domain;
using System.Collections.Concurrent;

namespace Common.Infrastructure.Net.Other;

public class NetService : INetService
{
    public ConcurrentDictionary<string, INet> NetMap { get; }


    public NetService()
    {
        NetMap = new ConcurrentDictionary<string, INet>();
    }
    public async Task<Result<byte[]>> ReadAsync(IReadConfig input)D
    {
        Result<byte[]>? result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
        {
            if (net.IsConnect) result=await net.ReadAsync(input);
        }
        return result??Result.Error<byte[]>("No connected net found");
    }

    public async Task<Result> WriteAsync(IWriteConfig input)
    {
        Result? result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
        {
            if (net.IsConnect) result = await net.WriteAsync(input);
        }
        return result?? new Result(false,"No connected net found");
    }

    public async Task<Result> ReConnectAsync(INetConfig input)
    {
        Result? result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
        {
            if (net.IsConnect == false) result = await net.ReConnectAsync();
        }
         return result?? new Result(false,"No connected net found");
    }

    public async Task<Result> AddConnectAsync(INet input)
    {
        Result? result = await input.ConnectAsync();
        if (result.IsSuccess) NetMap.TryAdd(input._netConfig.Ip, input);
        return result;
    }
}