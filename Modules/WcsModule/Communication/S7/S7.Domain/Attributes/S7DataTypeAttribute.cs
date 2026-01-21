namespace S7.Domain.Attributes;

[AttributeUsage(AttributeTargets.All, Inherited = false)]
public class S7DataTypeAttribute : Attribute
{
    public S7DataTypeAttribute(byte dataSize)
    {
        DataSize = dataSize;
    }
    /// <summary>
    /// 数据长度 
    /// 最长为255
    /// </summary>
    public byte DataSize { get; set; }
}