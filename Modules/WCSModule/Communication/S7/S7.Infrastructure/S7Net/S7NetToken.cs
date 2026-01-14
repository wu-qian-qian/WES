using Common.Application.Log;
using Common.Application.NET.Other.Base;
using Common.Application.NET.Other.Config;
using Common.Domain;
using Common.Domain.Log;
using Common.Helper;
using S7.Application.Abstractions.Data;
using S7.Net;

namespace S7.Infrastructure.S7Net;

public class S7NetToken : INet
{
    private S7.Net.Plc Plc { get; }
    public bool IsConnect => Plc?.IsConnected == true;
    public INetConfig _netConfig { get; }

    private readonly Action<string>? _logAciont;
    public S7NetToken(NetModel netConfig, Action<string> logAciont = null)
    {
        _netConfig = netConfig;
        _logAciont = logAciont;
        Plc = new S7.Net.Plc( netConfig.S7Type, netConfig.Ip, netConfig.Port, netConfig.Rack, netConfig.Solt);
        Plc.ReadTimeout = netConfig.ReadTimeOut;
        Plc.WriteTimeout = netConfig.WriteTimeOut;
    }

    private void WriteLog(string content)
    {
        _logAciont?.Invoke(content);
    }

    public async Task<Result> ConnectAsync()
    {
        if (Plc.IsConnected == true)
        {
            WriteLog($"{_netConfig.Ip}--重复连接PLC");
            return Result.Success();
        }
        Result? result =default;
        for (int i = 0; i < _netConfig.MaxRetries; i++)
        {
            try
            {
                await Plc.OpenAsync();
                WriteLog($"{_netConfig.Ip}--链接PLC成功");
                result = Result.Success();
            }
            catch (PlcException ex)
            {
                WriteLog($"{_netConfig.Ip}--{ex.Message}");
                await Task.Delay(_netConfig.DelayMs);
                WriteLog($"{_netConfig.Ip}--开始重试");
            }
        }
        return result?? Result.Error($"{_netConfig.Ip}--链接PLC失败");
    }

    public async Task<Result> ReConnectAsync()
    {
        WriteLog($"{_netConfig.Ip}--开始重新连接PLC");
        return await ConnectAsync();
    }

    public Task<Result> CloseAsync()
    {
        Result? result=default;
        try
        {
            Plc.Close();
            result = Result.Success();
        }
        catch (Exception e)
        {
            result = Result.Error(e.Message);
        }
        return Task.FromResult<Result>(result);
    }

    public async Task<Result<byte[]>> ReadAsync(IReadConfig input)
    {
        Result<byte[]>?  result= default;
        if (input is ReadModel readModel)
        {
            try
            {
               var bufferBlock = await
                Plc.ReadBytesAsync(readModel.S7BlockType
                    , readModel.DBAddress, readModel.DBStart, readModel.DBCount);
                result = Result<byte[]>.Success(bufferBlock);
            }
            catch (Exception ex)
            {
                WriteLog($"{_netConfig.Ip}--读取数据异常:{ex.Message}");
            }
        }
        else
        {
            WriteLog($"{_netConfig.Ip}--数据模型异常");
        }
        return result ?? Result.Error<byte[]>($"{_netConfig.Ip}--读取数据失败");
    }

    public async Task<Result> WriteAsync(IWriteConfig input)
    {
        Result? result = default;
        if (input is WriteModel writeModel)
        {
            if (writeModel.IsBit == false)
            {
                await Plc.WriteBytesAsync(writeModel.S7BlockType, writeModel.DBAddress, writeModel.DBStart, writeModel.Buffer);
                var buffer = Plc.ReadBytes(writeModel.S7BlockType, writeModel.DBAddress, writeModel.DBStart, writeModel.Buffer.Length);
                if (SequenceEquals.ByteSequenceEquals(buffer, writeModel.Buffer))
                {
                    result = Result.Success();
                }
            }
            else
            {
               await Plc.WriteBitAsync(writeModel.S7BlockType, writeModel.DBAddress, writeModel.DBStart, writeModel.BitAddress.Value,
                    writeModel.BitValue.Value);
                var @bool = Plc.Read(writeModel.S7BlockType, writeModel.DBAddress, writeModel.DBStart, VarType.Bit, writeModel.BitAddress.Value);
                if (writeModel.BitValue.Value.Equals(@bool))
                    result = Result.Success();
            }
        }
        else
        {
            WriteLog($"{_netConfig.Ip}--数据模型异常");
        }
        return result?? Result.Error($"{_netConfig.Ip}--写入数据失败");
    }


}