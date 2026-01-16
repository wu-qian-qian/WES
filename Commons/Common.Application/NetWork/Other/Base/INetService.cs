using System.Collections.Concurrent;
using Common.Application.NetWork.Other.Config;
using Common.Domain;

namespace Common.Application.NetWork.Other.Base;

public interface INetService
{
    ConcurrentDictionary<string, INet> NetMap { get; }

    Task<Result<byte[]>> ReadAsync(IReadConfig input);

    Task<Result> WriteAsync(IWriteConfig input);

    Task<Result> ReConnectAsync(INetConfig netDto);

    Task<Result> AddConnectAsync(INet connect);

    //todo 心跳操作
}