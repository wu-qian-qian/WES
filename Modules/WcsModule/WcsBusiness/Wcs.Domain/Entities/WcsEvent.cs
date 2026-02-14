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
    public string Content{get;set;}

    public string Description { get; set; }

    public LifeCircleType LifeCircle { get; set; }

    public DateTime OccurredAt { get; set; }
}