using Device.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conveyor
{
    internal class PipLineController : BaseCommonController<PipeLineDevice>, IController<PipeLineDevice>
    {
        public ValueTask ExecuteAsync(CancellationToken token = default)
        {
            //调状态机  输送设备类型+设备属性+，
            throw new NotImplementedException();
        }

        public PipeLineDevice GetDevice(string name)
        {
            throw new NotImplementedException();
        }
    }
}
