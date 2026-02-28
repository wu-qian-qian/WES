using Device.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public class PipeLineConfig:BaseDeviceConfig
    {
        public IEnumerable<PipeLineTypeEnume> PipeLineTypes { get; set; }
    }
}
