using Common.Domain.Entity;

/// <summary>
/// 子任务 
/// 由主任务通过任务任务模板进行匹配生成
/// </summary>
public class WcsTaskInfoDetail:BaseEntity
{
    public WcsTaskInfoDetail()
    {
        Id=Guid.NewGuid();
        TaskStatusType= TaskStatusTypeEnum.Craeted;
    }
    /// <summary>
    /// 任务模板编码， 关联TaskDetailConfig表的TaskTemplateCode字段
    /// 设备类型+任务模板编码+子任务状态 为一个状态机状态
    /// </summary>
    public string TaskTemplateCode { get; set; }
    public int Index { get; set; }

    /// <summary>
    /// 关联的WcsTaskInfo的Id
    /// </summary>
    public Guid TaskInfoId { get; set; }

    
    public string StartLocation { get; set; }

    public string EndLocation { get; set; }

    /// <summary>
    /// 任务是由什么设备执行
    /// </summary>
    public DeviceTaskTypeEnum DeviceTaskType { get;private set; }

    /// <summary>
    /// 只有待执行， 执行中 ，已完成 3种状态
    /// </summary>
    public TaskStatusTypeEnum TaskStatusType{get;set;}

    public string DeviceName { get; set; }

/// <summary>
/// 根据设备类型，任务模板编码，任务状态生成一个状态机的状态值
/// </summary>
/// <returns></returns>
    public string GetFSMValue()
    {
        return $"{DeviceTaskType}_{TaskTemplateCode}_{TaskStatusType}";
    }
    public void OnPending()
    {
        TaskStatusType = TaskStatusTypeEnum.Pending;
    }

    public void OnInProgress()
    {
        TaskStatusType = TaskStatusTypeEnum.InProgress;
    }

    public void OnCompleted()
    {
        TaskStatusType = TaskStatusTypeEnum.Completed;
    }
}