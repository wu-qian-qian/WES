namespace Common.Application.NET.Other.Config;

public interface INetConfig
{
    string Ip { get; }
    
    int Port { get; }
    
    int MaxRetries { get; }
    
    int DelayMs { get; }
}