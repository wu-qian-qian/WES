using Common.Application.NET.Other.Config;
using S7.Domain.Enums;



namespace S7.Application.Abstractions.Data;

public class WriteModel : IReadConfig
{
    public string Ip { get; set; }

    public S7BlockTypeEnum S7BlockType { get; set; }

    public int DBAddress { get; set; }

    public int DBStart { get; set; }

    public byte[]? Buffer { get; set; }

    public bool? IsBit { get; set; }

    public int? BitAddress { get; set; }

    public bool? BitValue { get; set; }

}