using Common.Application.FSM;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.FSM
{
    public class FSM : IFSM
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        public  Type CurrentState { get; private set; }

        public Type UpState { get; private set; }

        private readonly IServiceScope _serviceScope;

        /// <summary>
        /// 设置状态时的锁，保证同一时间只有一个线程在切换状态
        /// </summary>
        private readonly SemaphoreSlim slim=new SemaphoreSlim(1,1);
        public FSM(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;
        }
   
        /// <summary>
        /// 状态切换
        /// </summary>
        /// <param name="status"></param>
        public void SwitchStatus(Type status)
        {
            slim.Wait();
            try
            {
                if (CurrentState == status) return;
                if (CurrentState != null)
                {
                    UpState = CurrentState;
                }
                CurrentState = status;
            }
            finally
            {
                slim.Release();
            }
        }

        public async ValueTask OnExcute(string json, CancellationToken token = default)
        {
            if (UpState != null)
            {
                var status = _serviceScope.ServiceProvider.GetService(UpState);
                if (status is IStateMachine exiteState) await exiteState.ExitStateMachine(json);
            }

            if (CurrentState != null)
            {
                var status = _serviceScope.ServiceProvider.GetService(CurrentState);
                if (status is IStateMachine enterStatus)
                    await enterStatus.EnterStateMachine(json);
            }
        }

        public async ValueTask OnExcute(string key, string json, CancellationToken token = default)
        {
            IFsmManager? fsm=_serviceScope.ServiceProvider.GetService<IFsmManager>();
            if(fsm==null) throw new ArgumentNullException("状态机管理器未初始化");
            SwitchStatus(fsm.TryGetState(key));
            await OnExcute(json);
        }
    }
}
