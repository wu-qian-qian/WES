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
  /// 字段名
  /// </summary>
  public string DBValue{get;set;}
}

public  struct CacheEntityModel
{
  /// <summary>
  /// 字段名
  /// </summary>
  public string DBName{get;set;}
/// <summary>
/// 字段类型
/// </summary>
  public S7DataTypeEnum S7DataType { get; set; }

      /// <summary>
    ///     数组类型的长度
    /// </summary>
    public byte? ArrayLength { get; set; }

      /// <summary>
    ///     PLC的byte的偏移量
    ///     db3.2 -- S7DataTypeEnum的特性直
    /// </summary>
    public int DataOffset { get; set; }

    /// <summary>
    ///     bit的偏移量
    /// </summary>
    public byte? BitOffset { get; set; }
}
