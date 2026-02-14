using Common.Domain.Entity;

namespace Wcs.Domain.Entities;

/// <summary>
/// 任务解析时根据模板的区域编码进行流量管控
/// 
/// 区域 为各个独立的地方 如一层输送 2层输送
/// 
/// </summary>
public class Region: BaseEntity
{
    public Region()
    {
        this.Id = Guid.NewGuid();
    }

    public string RegionCode { get; set; }

    public string RegionDesc { get; set; }

    /// <summary>
    /// 是否限流
    /// </summary>
    public bool IsLimit { get; set; }

    /// <summary>
    /// 限流数量
    /// </summary>
    public int LimitCount { get; set; }
}