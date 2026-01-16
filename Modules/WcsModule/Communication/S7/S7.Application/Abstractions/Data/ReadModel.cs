using Common.Application.NET.Other.Config;
using S7.Domain.Enums;


namespace S7.Application.Abstractions.Data;

public class ReadModel : IReadConfig
{
    public string Ip { get; set; }

    /// <summary>
    /// DB 地址
    /// </summary>
    public int DBAddress { get; set; }
    /// <summary>
    /// Byte 位
    /// </summary>
    public int DBStart { get; set; }
    /// <summary>
    /// Byte 结束位
    /// </summary>
    public int DBCount { get; set; }

/// <summary>
/// 数据块的类型
/// </summary>
    public S7BlockTypeEnum S7BlockType { get; set; }
}