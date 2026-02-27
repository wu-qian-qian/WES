using Common.Application.Quartz;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conveyor
{
    internal class PipeLineJob : BaseJob
    {
        public override Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
