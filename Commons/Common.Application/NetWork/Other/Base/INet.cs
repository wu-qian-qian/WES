using Common.Application.NET.Other.Config;
using Common.Domain;

namespace Common.Application.NET.Other.Base;

public interface INet
{
    bool IsConnect { get; }
    public INetConfig _netConfig { get; }
    Task<Result> ConnectAsync();

    Task<Result> ReConnectAsync();

    Task<Result> CloseAsync();

    Task<Result<byte[]>> ReadAsync(IReadConfig input);

    Task<Result> WriteAsync(IWriteConfig bulkItem);
}