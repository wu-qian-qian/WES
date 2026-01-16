using Common.Application.NET.Other.Base;
using Common.Infrastructure.Net.Other;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using S7.Infrastructure.DataBase;
using S7.Presentation;

public static class InfrastructureConfiguration
{
  public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services,
  IConfiguration configuration)
  {
     services.AddSingleton<INetService, NetService>();    
     services.AddDbContext<PLCDBContext>(options =>{
            var connStr = configuration.GetConnectionString("default");
            options.UseSqlServer(connStr, builder =>
            builder.MigrationsHistoryTable(PLCDBContext.SchemasTable+ HistoryRepository.DefaultTableName));
            });
     return services;
  }

    public static void AddConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<ReadConsumer>();
        registrationConfigurator.AddConsumer<WriteConsumer>();
    }

/// <summary>
/// 初始化方法 当程序完全启动后被调用 主要用预加载一些数据信息
/// </summary>
/// <param name="serviceScope"></param>
/// <returns></returns>
    public static async Task InitializeAsync(IServiceScope serviceScope)
    {
      
    }
}