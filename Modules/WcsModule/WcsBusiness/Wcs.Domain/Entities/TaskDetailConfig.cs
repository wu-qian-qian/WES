using Common.Domain.Entity;

namespace Wcs.Domain.Entities
{
    public class TaskDetailConfig : BaseEntity
    {
       public TaskDetailConfig()
       {
       
       }

        /// <summary>
        /// 任务模板配置
        /// </summary>
       public string  TaskTemplateCode { get; set; }

       public string DeviceName { get; set; }

       public string StartLocation { get; set; }

       public string EndLocation { get; set; }

       public string RegionCode { get; set; }
    }
}