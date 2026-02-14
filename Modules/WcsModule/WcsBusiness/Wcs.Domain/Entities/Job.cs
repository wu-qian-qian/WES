using Common.Domain.Entity;

namespace Wcs.Domain.Entities;
public class Job : DomainEntity
{
    public Job():base(Guid.NewGuid())
    {
;
    }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string JobName { get; set; }
    /// <summary>
    /// 任务类型，必须是QuartzJobService中注册的类型
    /// </summary>
    public string JobType { get; set; }
    /// <summary>
    /// 任务描述
    /// </summary>
    public string? JobDescription { get; set; }
    /// <summary>
    /// Cron表达式，TimerInterval和IsRepeat不使用时必填
    /// </summary>
    public string? Corn{get;set;}
    /// <summary>
    /// 时间间隔，单位毫秒，Corn不使用时必填
    /// </summary>
    public int? TimerInterval { get; set; }

    /// <summary>
    /// 是否启用，Job的激活状态
    /// </summary>
    public bool IsActive { get; set; }

    public int TimerOut { get; set; }

    /// <summary>
    /// 是否重复执行，默认为true，设置为false时只执行一次
    /// </summary>
    public bool IsRepeat { get; set; }

    public void Activate(bool activate)
    {
        IsActive = activate;
        //发用领域事件 
    }

    public void Delete()
    {
        this.SoftDelete();
        //发用领域事件 吧job从调度器中剔除
    }
}