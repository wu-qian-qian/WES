using Common.Domain.Entity;

public class WcsTaskInfoDetail:BaseEntity
{
    public WcsTaskInfoDetail()
    {
        Id=Guid.NewGuid();
        TaskStatusType= TaskStatusTypeEnum.Craeted;
    }

    public int Index { get; set; }

    /// <summary>
    /// 关联的WcsTaskInfo的Id
    /// </summary>
    public Guid TaskInfoId { get; set; }

    public string StartLocation { get; set; }

    public string EndLocation { get; set; }

    public DeviceTaskTypeEnum DeviceTaskType { get;private set; }

    /// <summary>
    /// 只有待执行， 执行中 ，已完成 3种状态
    /// </summary>
    public TaskStatusTypeEnum TaskStatusType{get;set;}

    public string DeviceName { get; set; }

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