using Consul;
using Microsoft.Extensions.Hosting;

namespace Common.AspNetCore.Consul;

public class ConsulRegistrationService : IHostedService
{
    private readonly IHostApplicationLifetime _lifetime;
    private ConsulClient _consulClient;
    private readonly ConsulOptions _consulOptions;

    public ConsulRegistrationService(IHostApplicationLifetime lifetime, ConsulOptions consulOptions)
    {
        _lifetime = lifetime;
        _consulOptions = consulOptions;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consulClient = new ConsulClient(c => { c.Address = new Uri($"http://{_consulOptions.ConsulAddress}:8500"); });

        var registration = new AgentServiceRegistration()
        {
            ID = _consulOptions.ServiceId,
            Name = _consulOptions.ServiceName,
            Address = _consulOptions.ConsulAddress, // ⚠️ 关键，使用 IP 避免中文 Node
            Port = _consulOptions.Port,
            Check = new AgentServiceCheck()
            {
                HTTP = $"http://{_consulOptions.ServiceAddress}:5001/health", // 关键：使用容器名称
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromSeconds(5),
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(30)
            }
        };

        _consulClient.Agent.ServiceRegister(registration).Wait();

        _lifetime.ApplicationStopping.Register(() =>
        {
            _consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}