using System.Collections.Concurrent;
using Common.Application.NET.Other.Base;
using Common.Application.NET.Other.Config;
using Common.Domain;

namespace Common.Infrastructure.Net.Other;

public class NetService:INetService
{
    public ConcurrentDictionary<string, INet> NetMap { get; }

    
    public NetService()
    {
        NetMap = new ConcurrentDictionary<string, INet>();
    }
    public async Task<Result<byte[]?>?> ReadAsync(IReadConfig input)
    {
        Result<byte[]?>? result=default;
        if (NetMap.TryGetValue(input.Ip, out var net))
        {
            if (net.IsConnect)await net.ReadAsync(input);
        }
        return result;
    }

    public async  Task<Result> WriteAsync(IWriteConfig input)
    {
        Result result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
        {
            if (net.IsConnect)result = await net.WriteAsync(input);
        }
        return result;
    }

    public  async  Task<Result?> ReConnect(INetConfig input)
    {
        Result? result = default;
        if (NetMap.TryGetValue(input.Ip, out var net))
        { 
            if (net.IsConnect==false) result= await net.ReConnectAsync();
        }
        return result;
    }

    public async  Task<Result?> AddConnect(INet input)
    {
        Result? result= await input.ConnectAsync();
        if(result.IsSuccess)NetMap.TryAdd(input._netConfig.Ip, input);
        return result;
    }
}