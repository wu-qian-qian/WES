using Common.Application.NET.Other.Config;
using Common.Domain;
using System.Collections.Concurrent;

namespace Common.Application.NET.Other.Base;

public interface INetService
{
    ConcurrentDictionary<string, INet> NetMap { get; }

    Task<Result<byte[]>> ReadAsync(IReadConfig input);

    Task<Result> WriteAsync(IWriteConfig input);

    Task<Result> ReConnectAsync(INetConfig netDto);

    Task<Result> AddConnectAsync(INet connect);

    //todo 心跳操作
}