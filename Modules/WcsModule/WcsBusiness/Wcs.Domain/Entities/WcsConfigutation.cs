using Common.Domain.Entity;

namespace Wcs.Domain.Entities;

/// <summary>
/// WCS系统配置实体类
/// 如策略 优先 等其他配置
/// </summary>
public class WcsConfigutation:DomainEntity
{
  public WcsConfigutation():base(Guid.NewGuid())
  {
  }
  public string Key { get; set; }

  public string Value { get; set; }

  public void UpdateValue(string newValue)
  {
    if (newValue != Value)
    {
      Value = newValue;
      // 触发领域事件
    }
  }
}