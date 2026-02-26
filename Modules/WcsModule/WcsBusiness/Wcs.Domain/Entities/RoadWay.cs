using Common.Domain.Entity;

namespace Wcs.Domain.Entities;

/// <summary>
///   该设备类型的设备可以在直接那些目标点之间移动  ，这里的路线是每一个设备--》下一个设备  如果该设备没有下一设备就不该存在这条数据
///   如入库输送--堆垛机接货输送   
///    堆垛机接货输送--堆垛机执行
/// </summary>
public class RoadWay : BaseEntity
{
    public RoadWay()
    {
        Id=Guid.NewGuid();
    }
    public DeviceTypeEnum DeviceType { get; set; }

    /// <summary>
    /// 当前设备编码
    /// </summary>
     public string CurrentDeviceCode { get; set; }

     /// <summary>
     /// 目标设备编码
     /// </summary>
    public string TargetDeviceCode { get; set; }

   
   public string? Description { get; set; }

   public bool IsActive { get; set; }

    public Guid RegionId { get; set; }
    public Region Region { get; set; }
}