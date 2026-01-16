using Common.Application.NET.Other.Config;
using S7.Domain.Enums;


namespace S7.Application.Abstractions.Data;

public class NetModel : INetConfig
{
    public string Ip { get; set; }
    public int Port { get; set; }
    /// <summary>
    /// 重试次数
    /// </summary>
    public int MaxRetries { get; set; }
    /// <summary>
    /// 间隔时间
    /// </summary>
    public int DelayMs { get; set; }

    /// <summary>
    ///     Plc类型
    /// </summary>
    public S7TypeEnum S7Type { get; set; }

    /// <summary>
    ///     槽号
    /// </summary>
    public short Solt { get; set; }

    /// <summary>
    ///     机架
    /// </summary>
    public short Rack { get; set; }

    /// <summary>
    ///     读取超时
    /// </summary>
    public int ReadTimeOut { get; set; }

    /// <summary>
    ///     写入超时
    /// </summary>
    public int WriteTimeOut { get; set; }


    /// <summary>
    ///     读取心跳地址
    /// </summary>
    public string? ReadHeart { get; set; }

    /// <summary>
    ///     写入心跳地址
    /// </summary>
    public string? WriteHeart { get; set; }
}