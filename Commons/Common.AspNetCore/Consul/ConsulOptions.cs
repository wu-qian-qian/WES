namespace Common.AspNetCore.Consul;

public record ConsulOptions(
    string ServiceId,
    string ServiceName,
    string ServiceAddress,
    int Port,
    string ConsulAddress);