namespace Common.Application.FSM;

public interface IFsmManager
{
    ValueTask EnterStatus(string key, string json, CancellationToken token = default);
    void AddStates(string key, Type status);
}