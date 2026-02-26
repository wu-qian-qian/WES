using Common.Domain.Entity;
using static Common.Domain.AffairLifeCircle;

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

    public LifeCircleType LifeCircle { get; set; }

    /// <summary>
    /// 重试时间
    /// </summary>
    public DateTime RetryTime { get; set; }

    public int MaxRetry{get;set;}

    public int Retry{get;set;}

    public bool TryRetry(int timer=10)
    {
        if (Retry < MaxRetry)
        {
            Retry++;
            LifeCircle=LifeCircleType.Retry;
            RetryTime=DateTime.Now.AddSeconds(timer);
            return true;
        }
        return false;
    }
}