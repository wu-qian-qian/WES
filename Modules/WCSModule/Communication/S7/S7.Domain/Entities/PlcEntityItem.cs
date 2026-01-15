using Common.Domain.Entity;
using S7.Domain.Enums;
namespace S7.Domain;
public class PlcEntityItem : BaseEntity
{
    public PlcEntityItem()
    {
        Id=Guid.NewGuid();
    }

    public string Ip { get; set; }

    /// <summary>
    ///     PLC的的数据类型
    /// </summary>
    public S7DataTypeEnum S7DataType { get; set; }


    /// <summary>
    ///     dataBlock 数据块地址
    ///     db1,db2  就是1，2  m0 就是0
    /// </summary>
    public int DBAddress { get; set; }

    /// <summary>
    ///     PLC的byte的偏移量
    /// </summary>
    public int DataOffset { get; set; }

    /// <summary>
    ///     bit的偏移量
    /// </summary>
    public byte? BitOffset { get; set; }

    /// <summary>
    ///     PLC的存储类型
    /// </summary>
    public S7BlockTypeEnum S7BlockType { get; set; }

    /// <summary>
    ///     PLC索引
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    ///     字段名称
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     字段名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     数组类型的长度
    /// </summary>
    public byte? ArrayLength { get; set; }

    /// <summary>
    ///     关联设备
    /// </summary>
    public string DeviceName { get; set; }

    public bool IsUse { get; set; }

    /// <summary>
    ///     网络配置
    /// </summary>
    public Guid NetGuid { get; set; }
}