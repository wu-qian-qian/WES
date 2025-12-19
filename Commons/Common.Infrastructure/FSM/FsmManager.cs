using System.Diagnostics;
using System.Reflection;
using Common.Application.FSM;
using Common.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.FSM;

public class FsmManager : IFsmManager
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
        //开辟一个新的周期 防止生命周期污染
        var scop = _serviceProvider.CreateScope();
        //获取调用放状态标识  用来处理当前状态退出逻辑
        var exitAtt = AttributeHelper.GetDeclaringTypeAttribute<FsmAttribute>(2);
        var exitKey = exitAtt?.KeyName ?? throw new ArgumentException("无法捕获到当前状态");
        var statusType = _dicState[exitKey];
        var status = scop.ServiceProvider.GetService(statusType);
        if (status is IStateMachine exiteState) await exiteState.ExitStateMachine(json);
        //处理进入当前状态逻辑
        statusType = _dicState[key];
        status = scop.ServiceProvider.GetService(statusType);
        if (status is IStateMachine enterState) await enterState.EnterStateMachine(json);
    }

    public void AddStates(string key, Type status)
    {
        if (_dicState == null) throw new ArgumentNullException("未实例化数据结构");
        if (_dicState.ContainsKey(key)) throw new AggregateException("重复插入");
        _dicState[key] = status;
    }
}