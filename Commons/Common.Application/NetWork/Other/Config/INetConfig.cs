namespace Common.Application.NetWork.Other.Config;

public interface INetConfig
{
    string Ip { get; }

    int Port { get; }

    int MaxRetries { get; }

    int DelayMs { get; }
}