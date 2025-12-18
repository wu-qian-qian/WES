using System.Diagnostics;
using System.Reflection;
using Common.Application.FSM;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.FSM;

public class FsmManager:IFsmManager
{
    private readonly Dictionary<string, Type> _dicState;

    private readonly IServiceProvider _serviceProvider;


    public FsmManager(IServiceScopeFactory serviceScopeFactory)
    {
        _dicState = new Dictionary<string, Type>();
        _serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;
    }

    public async ValueTask EnterStatus(string key, string json, CancellationToken token = default)
    {
        if (token.IsCancellationRequested)
            token.ThrowIfCancellationRequested();
        var scop = _serviceProvider.CreateScope();
        var statusType = _dicState[GetUpStatusKey()];
        var status = scop.ServiceProvider.GetService(statusType);
        if (status is IStateMachine exiteState)
        {
            await exiteState.ExitStateMachine(json);
        }
        statusType = _dicState[key];
        status = scop.ServiceProvider.GetService(statusType);
        if (status is IStateMachine enterState)
        {
            await enterState.ExitStateMachine(json);
        }
    }

    private string GetUpStatusKey()
    {
        var stackTrace = new StackTrace();
        // Frame 0: GetUpStatusKey()
        // Frame 1: EnterStatus
        // EnterStatus 调用层
        var callerFrame = stackTrace.GetFrame(2);
        var callerType = callerFrame?.GetMethod()?.DeclaringType;
        var attr = callerType?.GetCustomAttribute<FsmAttribute>();
        return attr?.KeyName ?? string.Empty;
    }
    public void AddStates(string key, Type status)
    {
        if (_dicState == null) throw new ArgumentNullException("未实例化数据结构");
        if (_dicState.ContainsKey(key)) throw new AggregateException("重复插入");
        _dicState[key] = status;
    }
}