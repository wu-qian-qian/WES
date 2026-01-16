using S7.Domain.Enums;
using S7.Net;

namespace S7.Infrastructure.Helper;

/// <summary>
///     枚举转换帮助类
///     自定义的类型转换为S7.Net中的类型
/// </summary>
public class EnumHelper
{
    public static DataType S7BlockTypeToDataType(S7BlockTypeEnum @enum)
    {
        var dbType = @enum switch
        {
            S7BlockTypeEnum.DataBlock => DataType.DataBlock,
            S7BlockTypeEnum.Memory => DataType.Memory,
            S7BlockTypeEnum.Input => DataType.Input,
            _ => throw new AggregateException("无法解析")
        };
        return dbType;
    }

    public static VarType S7DataTypeToVarType(S7DataTypeEnum @enum)
    {
        var dbType = @enum switch
        {
            S7DataTypeEnum.Bool => VarType.Bit,
            S7DataTypeEnum.Byte => VarType.Byte,
            S7DataTypeEnum.Word => VarType.Word,
            S7DataTypeEnum.DWord => VarType.DWord,
            S7DataTypeEnum.Int => VarType.Int,
            S7DataTypeEnum.DInt => VarType.DInt,
            S7DataTypeEnum.Real => VarType.Real,
            S7DataTypeEnum.LReal => VarType.LReal,
            S7DataTypeEnum.String => VarType.String,
            S7DataTypeEnum.S7String => VarType.S7String,
            _ => throw new AggregateException("无法解析")
        };
        return dbType;
    }

    public static CpuType S7CpuTypeToVarType(S7TypeEnum @enum)
    {
        var dbType = @enum switch
        {
            S7TypeEnum.S71200 => CpuType.S71200,
            S7TypeEnum.S71500 => CpuType.S71500,
            _ => throw new AggregateException("无法解析")
        };
        return dbType;
    }
}