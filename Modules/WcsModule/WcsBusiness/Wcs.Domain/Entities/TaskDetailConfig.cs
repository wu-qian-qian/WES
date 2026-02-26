using Common.Domain.Entity;

namespace Wcs.Domain.Entities
{
    /// <summary>
    /// 任务详情配置
    /// 
    /// </summary>
    public class TaskDetailConfig : BaseEntity
    {
       public TaskDetailConfig()
       {
       
       }

        /// <summary>
        /// 任务模板配置
        /// 一个任务模板  
        /// </summary>
       public string  TaskTemplateCode { get; set; }

        /// <summary>
        /// 索引，表示在任务详情中的显示顺序
        /// </summary>
        public int Index { get; set; }
       /// <summary>
       /// 设备类型
       /// </summary>
       public DeviceTypeEnum DeviceType { get; set; }
    }
}