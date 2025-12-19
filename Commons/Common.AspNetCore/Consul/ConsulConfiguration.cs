using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Common.AspNetCore.Consul;

public static class ConsulConfiguration
{
    public static IServiceCollection AddConsulConfiguration(this IServiceCollection serviceCollection,
        ConsulOptions _consulOptions)
    {
        serviceCollection.AddSingleton<IHostedService, ConsulRegistrationService>(sp =>
        {
            var lifetime = sp.GetRequiredService<IHostApplicationLifetime>();
            return new ConsulRegistrationService(lifetime, _consulOptions);
        });
        return serviceCollection;
    }
}