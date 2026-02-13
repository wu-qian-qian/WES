using System.Reflection;
using Common.Application.Caching;
using Common.Infrastructure.Caching;
using Common.Infrastructure.DecoratorEvent;
using Common.Infrastructure.DependencyInjection;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.FSM;
using Common.Infrastructure.Log;
using Common.Infrastructure.MediatR;
using Common.Infrastructure.NetWork;
using Common.Infrastructure.Quartz;
using Microsoft.Extensions.DependencyInjection;


namespace Common.Infrastructure;

public static class InfranstructureConfiguration
{
    /// <summary>
    /// 程序唯一模块注入
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="assArray"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfranstructureConfiguration(this IServiceCollection serviceCollection,
       ModulesOptions options)
    {
        //带有装饰器 的事件总线
        if(options.DecoratorEvent)
            serviceCollection.AddDecoratorConfiguration(options.ApplicationAssemblyArr);
        //依赖特性注入
        if(options.AutoInject)
             serviceCollection.AddDependyConfiguration(options.ApplicationAssemblyArr);
        //事件总线注入
        if(options.EventBus)
            serviceCollection.AddEventBusConfiguration(options.ApplicationAssemblyArr);
        //状态机注入
        if(options.Fsm)
            serviceCollection.AddFsmConfiguration(options.ApplicationAssemblyArr);
        //日志配置
         if(options.logConfigActions==null)
            serviceCollection.AddSeriLogConfiguration();
        else
            serviceCollection.AddSeriLogConfiguration(options.logConfigActions);
        //Job注入
        if(options.QuartzJob)
            serviceCollection.AddQuatrzJobConfiguration();
        if(options.Cache)
            serviceCollection.AddCacheConfiguration(options.RedisConnString);
        if(options.Http)
            serviceCollection.AddHttpConfiguration(options.HttpOptions);
        if(options.MediatR)
            serviceCollection.AddMediatRConfiguration(options.ApplicationAssemblyArr,options.behaviors);
        return serviceCollection;
    }
}