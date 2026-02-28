using Device.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Domain.Entities;

namespace Pipeline
{
    internal class PipeLineDevice : BaseDevice<PipeLineConfig, PipeLineDBEntity, WcsTaskInfoDetail>
    {
        public PipeLineDevice(string name, bool enable) : base(enable)
        {
            Name = name;
        }

        public PipeLineTypeEnume CurrentPipelineType{get;private set;}
        public override PipeLineDBEntity DBEntity { get; protected set; }
        public override PipeLineConfig Config { get; protected set; }
        public override WcsTaskInfoDetail WcsTask { get; protected set; }

        public void SetCurrentPiplineType(PipeLineTypeEnume currentPipelineType)
        =>this.CurrentPipelineType=currentPipelineType;
    }
}
