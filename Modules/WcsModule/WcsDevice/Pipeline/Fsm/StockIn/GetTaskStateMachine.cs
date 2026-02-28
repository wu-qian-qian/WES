using Common.Application.FSM;

namespace Conveyor.FSM.StockIn
{
    [Fsm($"{nameof(DeviceTypeEnum.PipeLine)}-{nameof(PipeLineTypeEnume.StockInPort)}-{nameof(FSMTypeEnum.GetWcsTask)}")]
    internal class GetTaskStateMachine : IStateMachine
    {
        public ValueTask EnterStateMachine(string data, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask ExitStateMachine(string data, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}