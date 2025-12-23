using System.Collections.Concurrent;
using Common.Application.NET.Other.Config;
using Common.Domain;

namespace Common.Application.NET.Other.Base;

public interface INetService
{
    ConcurrentDictionary<string, INet> NetMap { get; }
    
    Task<Result<byte[]?>?> ReadAsync(IReadConfig input);

    Task<Result> WriteAsync(IWriteConfig input);
    
    Task<Result?> ReConnect(INetConfig netDto);

    Task<Result?> AddConnect(INet connect);
    
    //todo 心跳操作
}