using Common.Domain.Entity;
namespace Wcs.Domain.Entities;
public class Location:BaseEntity
{
    public Location()
    {
        Id=Guid.NewGuid();
    }
    public string Code { get; set; }
    public string Description { get; set; }

    
    /// <summary>
    /// 位置信息，如坐标，目标编号等
    /// 公式位排-列-层
    /// 如：1-2-3 表示1排2列3层
    /// 编码则为 1001 目标位置
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// 区域编码
    /// 可理解位巷道
    /// </summary>
    public string AreaCode { get; set; }

    /// <summary>
    /// 位置类型，如货位
    /// </summary>
    public LocationTypeEnum LocationType { get; set; }
    /// <summary>
    /// 位置状态，如空闲，占用，异常等
    /// </summary>
    public LocationStatusTypeEnum Status { get; set; }

    /// <summary>
    /// 位置尺寸类型，如小中大等
    /// </summary>
    public LocationSizeTypeEnum locationSizeType { get; set; }
}