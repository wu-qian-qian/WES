namespace Common.Application.FSM
{
    public interface IFSM
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        Type CurrentState { get; }

        Type UpState { get;  }
        void SwitchStatus(Type status);

        ValueTask OnExcute(string json, CancellationToken token = default);

        ValueTask OnExcute(string key,string json, CancellationToken token = default);
    }
}
