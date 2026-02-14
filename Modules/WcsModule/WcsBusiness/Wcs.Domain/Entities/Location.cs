using Common.Domain.Entity;
namespace Wcs.Domain.Entities;
public class Location:BaseEntity
{
    public Location()
    {
        Id=Guid.NewGuid();
    }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// 位置信息，如坐标
    /// </summary>
    public string? Position { get; set; }

/// <summary>
/// 区域编码
/// 可理解位巷道
/// </summary>
    public string AreaCode { get; set; }
}