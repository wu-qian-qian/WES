using Common.Application.FSM;
using Common.Helper;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace Common.Infrastructure.FSM;

public class FsmManager : IFsmManager
{
    private readonly Dictionary<string, Type> _dicState;

    private readonly ConcurrentDictionary<string, IFSM> _fsmPairs;

    private readonly IServiceScopeFactory  _scopeFactory;


    public FsmManager(IServiceScopeFactory serviceScopeFactory)
    {
        _dicState = new Dictionary<string, Type>();
        _scopeFactory = serviceScopeFactory;
    }

    public async ValueTask SwitchStatus(string owner,string key,string json, CancellationToken token = default)
    {
         _fsmPairs.TryGetValue(owner, out var pair);
        if (pair != null)
        {
            pair.SwitchStatus(TryGetState(key));
            await pair.OnExcute(json, token);
        }
    }

    public void AddStates(string key, Type status)
    {
        if (_dicState == null) throw new ArgumentNullException("未实例化数据结构");
        if (_dicState.ContainsKey(key)) throw new AggregateException("重复插入");
        _dicState[key] = status;
    }

    public Type TryGetState(string key)
    {
        if (_dicState == null) throw new ArgumentNullException("未实例化数据结构");
        if (_dicState.TryGetValue(key, out var status)) return status;
        throw new KeyNotFoundException("未找到对应状态");
    }
    public IFSM RegistrationFsm(string owner)
    {
        var scope= _scopeFactory.CreateScope();
        var fms = new FSM(scope);
        return fms;
    }
}