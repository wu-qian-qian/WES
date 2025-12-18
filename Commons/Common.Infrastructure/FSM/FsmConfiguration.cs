using System.Reflection;
using Common.Application.FSM;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.FSM;

public static class FsmConfiguration
{
    public static IServiceCollection AddFsmConfiguration(this IServiceCollection serviceCollection, Assembly[] assArray)
    {
        if (assArray == null || assArray.Length == 0)
            throw new ArgumentException("At least one assembly must be provided.", nameof(assArray));
        var dictionary = new Dictionary<string, Type[]>();
        var assTypeList = assArray.Select(p => p.GetTypes());
        var types = new List<Type>();
        foreach (var item in assTypeList) types.AddRange(item);

        var stateMachines = types.Where(type => type is { IsAbstract: false, IsInterface: false } &&
                                                type.IsAssignableTo(typeof(IStateMachine))).ToArray();
        foreach (var handler in stateMachines) serviceCollection.AddTransient(handler);
        serviceCollection.AddSingleton<IFsmManager>(sp =>
        {
            var serviceScopeFac = sp.GetRequiredService<IServiceScopeFactory>();
            IFsmManager manager = new FsmManager(serviceScopeFac);
            for (var i = 0; i < stateMachines.Length; i++)
            {
                var stateMachine = stateMachines[i];
                var att = stateMachine.GetCustomAttribute<FsmAttribute>();
                if (att == null) continue;
                manager.AddStates(att.KeyName, stateMachine);
            }
            return manager;
        });
        return serviceCollection;
    }
}