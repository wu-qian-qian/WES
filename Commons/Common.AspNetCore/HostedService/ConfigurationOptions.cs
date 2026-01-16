using Microsoft.Extensions.DependencyInjection;

namespace Common.AspNetCore.HostedService;

public sealed class ConfigurationOptions
{
    public IEnumerable<Func<IServiceScope, Task>> Configurations { get; private set; }

    internal void AddConfiguration(Func<IServiceScope, Task>[] configurations)
    {
        Configurations = configurations;
    }
}