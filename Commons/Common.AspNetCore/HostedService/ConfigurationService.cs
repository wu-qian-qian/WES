using Microsoft.Extensions.DependencyInjection;

public sealed class ConfigurationService
{
    public IEnumerable<Func<IServiceScope,Task>> Configurations{get;private set;}

    internal void AddConfiguration(Func<IServiceScope,Task>[] configurations)
    {
        Configurations= configurations;
    }
}