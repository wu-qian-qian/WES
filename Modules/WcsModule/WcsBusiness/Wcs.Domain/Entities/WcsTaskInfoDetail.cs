using Common.Domain.Entity;

namespace Wcs.Domain.Entities;
/// <summary>
/// 子任务 
/// 由主任务通过任务任务模板进行匹配生成
/// 
/// 状态机将数据写入PLC后任务变为执行中
/// 然后发送一个WcsEvent事件
/// 由消费者取定时消费这个WcsEvent事件
/// 
/// 该事件主要是来判断任务是否完成，如果完成了则触发当前状态机的完成状态 实现逻辑闭环
/// </summary>
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

    
    public string? StartLocation { get; set; }

    public string? EndLocation { get; set; }

    /// <summary>
    /// 任务是由什么设备执行
    /// </summary>
    public DeviceTypeEnum DeviceTaskType { get;private set; }

    /// <summary>
    /// 只有待执行， 执行中 ，已完成 3种状态
    /// </summary>
    public TaskStatusTypeEnum TaskStatusType{get;set;}

    public string DeviceName { get; set; }
    public WcsTaskInfo WcsTaskInfo { get; set; }

    /// <summary>
    /// 根据设备类型，任务模板编码，任务状态生成一个状态机的状态值
    /// </summary>
    /// <returns></returns>
    public string GetFSMValue()
    {
       // return $"{DeviceTaskType}_{TaskTemplateCode}_{TaskStatusType}";
       //TODO
       return string.Empty;
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

    /// <summary>
    /// 是否需要申请终点
    /// </summary>
    /// <returns></returns>
    public bool IsApplyEndLocation()
    {
       return string.IsNullOrEmpty(EndLocation);
    }
}