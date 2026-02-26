using Common.Domain.Entity;

namespace Wcs.Domain.Entities
{
    /// <summary>
    /// 任务详情配置
    /// 
    /// </summary>
    public class TaskTemplate : BaseEntity
    {
       public TaskTemplate()
       {
       
       }

        /// <summary>
        /// 任务模板配置
        /// 一个任务模板  
        /// </summary>
       public string  TaskTemplateCode { get; set; }

        /// <summary>
        /// 索引，表示在任务详情中的显示顺序
        /// ， 通过索引来区分不同设备类型的任务执行顺序
        /// </summary>
        public int Index { get; set; }
       /// <summary>
       /// 设备类型
       /// </summary>
       public DeviceTypeEnum DeviceType { get; set; }

     /// <summary>
     /// 是否激活
     /// </summary>
       public bool IsActive { get; set; }
        
        public string? Description { get; set; }
    }
}