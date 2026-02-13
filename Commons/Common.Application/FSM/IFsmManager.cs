namespace Common.Application.FSM;

public interface IFsmManager
{
    Type TryGetState(string key);
    IFSM RegistrationFsm(string owner);
    ValueTask SwitchStatus(string owner,string key, string json, CancellationToken token = default);
    void AddStates(string key, Type status);
}