using Common.Application.Log;
using Common.Application.NET.Other.Base;
using Common.Application.NET.Other.Config;
using Common.Domain;
using Common.Domain.Log;
using Common.Helper;
using NPOI.Util;
using S7.Application.Abstractions.Data;
using S7.Application.Events.ReadBuffer;
using S7.Domain.Enums;
using S7.Net;
using S7.Net.Types;
using Byte = System.Byte;

namespace S7.Infrastructure.S7Net;

public class S7NetToken:INet
{
    private S7.Net.Plc Plc { get;}
    public bool IsConnect => Plc?.IsConnected == true;
    public INetConfig _netConfig { get; }

    private readonly ILogService _logService;
    public S7NetToken(NetModel netConfig,ILogService logService=null)
    {
        _netConfig = netConfig;
        _logService = logService;
        var s7Cpu = netConfig.S7Type switch
        {
            S7TypeEnum.S71200 => CpuType.S71200,
            S7TypeEnum.S71500 => CpuType.S71500,
            _ => throw new ArgumentException("无可用类型")
        };
        Plc = new S7.Net.Plc(s7Cpu, netConfig.Ip, netConfig.Port, netConfig.Rack, netConfig.Solt);
        Plc.ReadTimeout = netConfig.ReadTimeOut;
        Plc.WriteTimeout = netConfig.WriteTimeOut;
    }

    private void WriteLog(string content)
    {
        _logService.WriteInformationLog(LogCategoryType.Communication,content);
    }
    
    public async Task<Result?> ConnectAsync()
    {
        if (Plc.IsConnected == true)
        {
            WriteLog($"{_netConfig.Ip}--重复连接PLC");
            return Result.Success();
        }
        Result? result=Result.Error("连接失败");
        for (int i = 0; i < _netConfig.MaxRetries; i++)
        {
            try
            {
                await Plc.OpenAsync();
                WriteLog($"{_netConfig.Ip}--链接PLC成功");
                result=Result.Success();
            }
            catch (PlcException ex)
            {
                WriteLog($"{_netConfig.Ip}--{ex.Message}");
                await Task.Delay(_netConfig.DelayMs);
                WriteLog($"{_netConfig.Ip}--开始重试");
            }
        }
        return result;
    }

    public async Task<Result?> ReConnectAsync()
    {
        WriteLog($"{_netConfig.Ip}--开始重新连接PLC");
        return  await ConnectAsync();
    }

    public  Task<Result?> CloseAsync()
    {
        Result? result;
        try
        {
            Plc.Close();
            result= Result.Success();
        }
        catch (Exception e)
        {
            result = Result.Error(e.Message);
        }
        return Task.FromResult<Result>(result);
    }

    public async Task<Result<byte[]?>?> ReadAsync(IReadConfig input)
    {
        byte[]? bufferBlock = default;
        if (input is ReadModel readModel)
        {
            bufferBlock = await 
                Plc.ReadBytesAsync(S7.Infrastructure.Helper.EnumHelper.S7BlockTypeToDataType(readModel.S7BlockType)
                    , readModel.DBAddress, readModel.DBStart, readModel.DBCount);
        }
        else
        {
            WriteLog($"{_netConfig.Ip}--数据模型异常");
        }
        return Result.Success<byte[]?>(bufferBlock);
    }

    public async Task<Result> WriteAsync(IWriteConfig input)
    {
        Result result = default;
        if (input is WriteModel writeModel)
        {
            var dbType= S7.Infrastructure.Helper.EnumHelper.S7BlockTypeToDataType(writeModel.S7BlockType);
            if(writeModel.IsBit==false)
            {
                await Plc.WriteBytesAsync(dbType, writeModel.DBAddress, writeModel.DBStart, writeModel.Buffer); 
                var buffer = Plc.ReadBytes(dbType, writeModel.DBAddress, writeModel.DBStart, writeModel.Buffer.Length);
                if (SequenceEquals.ByteSequenceEquals(buffer, writeModel.Buffer))
                {
                    result = Result.Success();
                }
            }
            else
            {
                Plc.WriteBitAsync(dbType, writeModel.DBAddress, writeModel.DBStart, writeModel.BitAddress.Value,
                    writeModel.BitValue.Value);
                var @bool = Plc.Read(dbType, writeModel.DBAddress, writeModel.DBStart, VarType.Bit, writeModel.BitAddress.Value);
                if(writeModel.BitValue.Value.Equals(@bool))
                    result=Result.Success();
            }
        }
        else
        {
            WriteLog($"{_netConfig.Ip}--数据模型异常");
        }

        return result;
    }
    
 
}