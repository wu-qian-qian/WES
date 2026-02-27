using Common.Domain.Entity;


namespace Wcs.Domain.Entities;
public class WcsEvent : BaseEntity
{
    public WcsEvent()
    {
        Id=Guid.NewGuid();
    }

    /// <summary>
    /// 事件ID
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content{get;set;}

    public string? Description { get; set; }

    public EventLifeCircleType LifeCircle { get; set; }

    /// <summary>
    /// 重试时间
    /// </summary>
    public DateTime RetryTime { get; set; }

    public int MaxRetry{get;set;}

    public int Retry{get;set;}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timer">最好使用随机抖动</param>
    /// <returns></returns>
    public bool TryRetry(int timer=10)
    {
        if (Retry < MaxRetry)
        {
            Retry++;
            LifeCircle=EventLifeCircleType.Retry;
            RetryTime=DateTime.Now.AddSeconds(timer);
            return true;
        }
        return false;
    }
}