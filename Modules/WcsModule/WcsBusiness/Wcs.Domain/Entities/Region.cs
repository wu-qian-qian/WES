using Common.Domain.Entity;

namespace Wcs.Domain.Entities;

/// <summary>
/// 
/// </summary>
public class Region : BaseEntity
{
    public Region()
    {
        Id=Guid.NewGuid();
    }
    /// <summary>
    /// 区域主要 多条路线可能在同一区域 所以需要一个区域的概念来进行限流
    /// </summary>
    public string RegionCode { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// 是否限流
    /// </summary>
    public bool IsLimit { get; set; }

    /// <summary>
    /// 组编码、因为可能有多个入库点但是每一个入库点都走用一段路径，此路径又需要限制流量所以
    /// </summary>
    public string GroupCode { get; set; }
    /// <summary>
    /// 限流数量
    /// </summary>
    public int LimitCount { get; set; }

        /// <summary>
        /// 当前数量
        /// </summary>
    public int CurrentCount { get; set; }

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

    public bool IsPass()
    {
        if (!IsLimit)
        {
            return true;
        }
        return CurrentCount < LimitCount;
    }
}