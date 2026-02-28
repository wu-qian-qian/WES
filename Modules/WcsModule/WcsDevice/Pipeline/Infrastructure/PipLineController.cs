using Device.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    internal class PipLineController : BaseController<PipeLineDevice>
    {

        public override PipeLineDevice[] Devices {get;}
        public override ValueTask ExecuteAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
