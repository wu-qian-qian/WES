using S7.Domain.Enums;

namespace S7.Application;
/// <summary>
/// 变量模型的缓存
/// </summary>
public  struct ReadCacheEntityModel
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

public struct WriteCacheEntityModel 
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
    ///     PLC的byte的偏移量
    ///     db3.2 -
    /// </summary>
    public int DataOffset { get; set; }

    /// <summary>
    ///     bit的偏移量
    /// </summary>
    public byte? BitOffset { get; set; }

    public string Ip { get; set; }

    public S7BlockTypeEnum S7BlockType { get; set; }

    public int DBAddress { get; set; }

    public int ArrayLen{get;set;}

}