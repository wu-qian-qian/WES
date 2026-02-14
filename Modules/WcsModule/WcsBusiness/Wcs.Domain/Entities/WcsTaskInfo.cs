using Common.Domain.Entity;

public class WcsTaskInfo : DomainEntity
{
    public WcsTaskInfo():base(Guid.NewGuid())
    {
        
    }

    public string TaskCode { get; set; }
    public TaskTypeEnum TaskType { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }

    /// <summary>
    /// 起点位置
    /// </summary>
    public string StartLocation { get; set; }
    /// <summary>
    /// 终点位置
    /// </summary>
    public string EndLocation { get; set; }
    public string TemplateCode { get; set; }

    /// <summary>
    /// 任务序列号
    /// </summary>
    public int SerialNumber { get; set; }
    public TaskStatusTypeEnum Status { get; set; }

    public void UpdateStatus(TaskStatusTypeEnum newStatus)
    {
        // 可以在这里添加状态转换的验证逻辑 
        Status = newStatus;
    }
    /// <summary>
    /// 任务详情列表
    /// </summary>
    public ICollection<WcsTaskInfoDetail> Details { get; set; }
}