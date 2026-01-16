using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common.AspNetCore.HostedService;

/// <summary>
///     主要用来初始化配置服务
/// </summary>
public class InitielizeConfigurationService(
    IServiceScopeFactory _scopeFactory,
    Logger<InitielizeConfigurationService> logger,
    ConfigurationOptions _configurationService) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var _scope = _scopeFactory.CreateScope();
        if (_configurationService.Configurations != null)
            try
            {
                foreach (var config in _configurationService.Configurations) await config(_scope);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "初始化配置服务失败");
            }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("初始化配置服务停止");
        return Task.CompletedTask;
    }
}