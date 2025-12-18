namespace Common.Application.FSM;

public interface IStateMachine
{
    ValueTask EnterStateMachine(string data, CancellationToken token = default);

    ValueTask ExitStateMachine(string data, CancellationToken token = default);
}