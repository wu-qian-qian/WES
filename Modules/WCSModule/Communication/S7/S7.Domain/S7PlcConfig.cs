using Common.Domain.Entity;
using S7.Domain.Enums;

namespace S7.Domain;

/// <summary>
/// dotnet new classlib -n MyMathLibrary
/// </summary>
public class S7PlcConfig:BaseEntity
{
    public S7PlcConfig()
    {
        Id=Guid.NewGuid();
    }
    public string Ip { get; protected set; }

    public int Port { get; protected set; }
    public S7TypeEnum1 S7Type { get; set; }

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
    ///     是否启用
    /// </summary>
    public bool IsUse { get; set; }

    /// <summary>
    ///     读取心跳地址
    /// </summary>
    public string? ReadHeart { get; set; }

    /// <summary>
    ///     写入心跳地址
    /// </summary>
    public string? WriteHeart { get; set; }


    public void UpdateIp(string ip)
    {
        Ip = ip;
    }

    public void UpdatePort(int port)
    {
        Port = port;
    }
}
