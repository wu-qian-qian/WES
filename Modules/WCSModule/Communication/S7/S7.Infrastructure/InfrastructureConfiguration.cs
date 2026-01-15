using Common.Application.NET.Other.Base;
using Common.Infrastructure.Net.Other;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureConfiguration
{
  public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services)
  {
     services.AddSingleton<INetService, NetService>();    
     return services;
  }
}