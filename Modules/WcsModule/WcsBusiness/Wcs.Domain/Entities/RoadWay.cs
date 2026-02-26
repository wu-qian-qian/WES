using Common.Domain.Entity;

namespace Wcs.Domain.Entities;

/// <summary>
///   该设备类型的设备可以在直接那些目标点之间移动
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

    /// <summary>
    /// 是否限流
    /// </summary>
    public bool IsLimit { get; set; }

    /// <summary>
    /// 限流数量
    /// </summary>
    public int LimitCount { get; set; }

        /// <summary>
        /// 当前数量
        /// </summary>
    public int CurrentCount { get; set; }


   public string Description { get; set; }

   public bool IsActive { get; set; }

   public void AddLimitCount()
   {
       CurrentCount++;
   }
    public void ReduceLimitCount()
    {
         if (CurrentCount > 0)
         {
              CurrentCount--;
         }
    }
}