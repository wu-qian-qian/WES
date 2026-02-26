using Common.Domain.Entity;
namespace Wcs.Domain.Entities;
public class WcsTaskInfo : DomainEntity
{
    public WcsTaskInfo():base(Guid.NewGuid())
    {
        
    }
    public string TaskCode { get; set; }
    public string ContainerCode { get; set; }

    
    public TaskTypeEnum TaskType { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }


    /// <summary>
    ///     是否强制执行
    /// </summary>
    public bool IsEnforce { get; set; }
    /// <summary>
    /// 起点位置
    /// </summary>
    public string? StartLocation { get; set; }
    /// <summary>
    /// 终点位置
    /// </summary>
    public string? EndLocation { get; set; }
    public string TemplateCode { get; set; }

    /// <summary>
    /// 任务序列号
    /// </summary>
    public int SerialNumber { get; set; }
    public TaskStatusTypeEnum Status { get; set; }

    /// <summary>
    /// 任务系统类型，如WCS， WMS等
    /// </summary>
    public TaskSystemTypeEnum TaskSystemType { get; set; }

    public void UpdateStatus(TaskStatusTypeEnum newStatus)
    {
        // 可以在这里添加状态转换的验证逻辑 
        Status = newStatus;
    }
    /// <summary>
    /// 任务详情列表
    /// </summary>
    public ICollection<WcsTaskInfoDetail> Details { get; set; }

    public void UpdateTaskStatus(TaskStatusTypeEnum newStatus)
    {
        // 可以在这里添加状态转换的验证逻辑 
        if (newStatus != Status)
        {
            Status = newStatus;
            // 触发领域事件
        }
    }
}