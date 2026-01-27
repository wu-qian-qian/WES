using S7.Domain.Enums;

namespace S7.Application.Handlers;
/// <summary>
/// DB块 字段与值的 存储数据结构
/// </summary>
public  struct EntityModel
{
    /// <summary>
  /// 字段名
  /// </summary>
  public string DBName{get;set;}

  /// <summary>
  /// 字段数据
  /// </summary>
  public string DBValue{get;set;}
}
