
using System.Reflection;
using Common.Infrastructure.NetWork.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


/// <summary>
/// 系统模块配置类
/// </summary>
public sealed class ModulesOptions
{
    public ModulesOptions()
    {

    }
    /// <summary>
    /// 程序一些模块的程序集
    /// </summary>
    public Assembly[] ApplicationAssemblyArr{get;set;}

    /// <summary>
    /// 装饰器事件总线
    /// </summary>
    public bool DecoratorEvent{get;set;}

    /// <summary>
    /// 特性自动注入
    /// </summary>
    public bool AutoInject{get;set;}

/// <summary>
/// 事件总线
/// 
/// </summary>
    public bool EventBus{get;set;}

/// <summary>
/// 状态机
/// </summary>
    public bool Fsm{get;set;}

    public  Action<LoggerConfiguration>[] logConfigActions;
/// <summary>
/// Job
/// </summary>
    public bool QuartzJob{get;set;}
    
    public bool Cache{get;set;}

    public string RedisConnString{get;set;}
    
    public bool Http{get;set;}

    public HttpOptions HttpOptions{get;set;}

/// <summary>
/// 中介者包注入
/// </summary>
    public bool MediatR{get;set;}

   public Action<MediatRServiceConfiguration>[] behaviors;
}