using Common.Application.NET.Other.Config;
using S7.Net;

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
    /// bit 位
    /// </summary>
    public byte? DBBit { get; set; }

    public DataType S7BlockType { get; set; }
}