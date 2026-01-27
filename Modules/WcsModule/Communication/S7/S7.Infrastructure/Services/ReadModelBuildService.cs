using System.Collections.Concurrent;
using System.Text;
using Common.Helper;
using Common.TransferBuffer;
using S7.Application.Abstractions.Data;
using S7.Application.Handlers;
using S7.Application.Services;
using S7.Domain.Attributes;
using S7.Domain.Entities;
using S7.Domain.Enums;

namespace S7.Infrastructure.Service;

public class ReadModelBuildService : IReadModelBuildService
{
    /// <summary>
    /// 用来缓存 读取db块的数据模型
    /// 设备名+DB块地址+IP+数据块类型
    /// </summary>
    private static ConcurrentDictionary<string,IEnumerable<S7ReadModel>> _readModeMap=new();
    /// <summary>
    /// 用来缓存 数据块转换为字段的模型
    /// </summary>
     private static ConcurrentDictionary<string,IEnumerable<CacheEntityModel>> _readEntityMap=new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="key">设备名+数据块位置+数块类型</param>
    /// <returns></returns>
    public Task<IEnumerable<EntityModel>> ReadEntityModelBuildAsync(byte[] buffer, string key)
    {
        IEnumerable<CacheEntityModel> entityModels=_readEntityMap[key];
        var tempModel= entityModels.Select(p =>
        {
            EntityModel model=new EntityModel();
            model.DBName=p.DBName;
            model.DBValue=TransferBufferToData(buffer,p.DataOffset,p.BitOffset,p.S7DataType,p.ArrayLength);
           return model; 
        });
        return Task.FromResult(tempModel);
    } 

    public Task<IEnumerable<S7ReadModel>> ReadPlcModelBuildAsync(string deviceName)
    {
          return Task.FromResult(_readModeMap[deviceName]); 
    }

/// <summary>
/// 缓存加载
/// 
/// 主要用来减少数据库的访问
/// 当设备多的时候频繁的访问数据会造成执行效率的减少
/// </summary>
/// <param name="plcEntityList"></param>
/// <returns></returns>
/// <exception cref="NotImplementedException"></exception>
    public Task LoadAsync(IEnumerable<PlcEntityItem> plcEntityList)
    {
        _readModeMap.Clear();
        _readEntityMap.Clear();
        var plcEntityGroups = plcEntityList.GroupBy(p => new { p.DeviceName,p.Ip, p.DBAddress, p.S7BlockType });
        foreach (var plcEntityGroup in plcEntityGroups)
        {
            List<S7ReadModel> tempNodeList=new List<S7ReadModel>();
            if(_readModeMap.TryGetValue(plcEntityGroup.Key.DeviceName,out var nodeList))
            {
              tempNodeList.AddRange(nodeList);
            }
            var read = new S7ReadModel();
            read.Ip = plcEntityGroup.Key.Ip;
            read.DBAddress = plcEntityGroup.Key.DBAddress;
            read.S7BlockType = plcEntityGroup.Key.S7BlockType;
            read.DBStart = plcEntityGroup.MinBy(p => p.Index).DataOffset;
            var dataType = plcEntityGroup.MaxBy(p => p.Index)?.S7DataType;
            var index = dataType?.GetEnumAttribute<S7DataTypeAttribute>()?.DataSize;
            read.DBCount = plcEntityGroup.MaxBy(p => p.Index).DataOffset + index.Value;
            tempNodeList.Add(read);
            _readModeMap.TryAdd(plcEntityGroup.Key.DeviceName,tempNodeList.ToArray());
            //数据模型组装
            var key=plcEntityGroup.Key.DeviceName+plcEntityGroup.Key.DBAddress+plcEntityGroup.Key.Ip
            +plcEntityGroup.Key.S7BlockType;
             var entityModels=plcEntityGroup.ToList()
             .Select(p =>
             {
                return new CacheEntityModel()
                {
                    DataOffset=p.DataOffset,
                    DBName=p.Name,
                    BitOffset=p.BitOffset,
                    ArrayLength=p.ArrayLength,
                    S7DataType=p.S7DataType,
                 
                };
             });
            _readEntityMap.TryAdd(key,entityModels);
        }
        return Task.CompletedTask;
    }

/// <summary>
/// 数据类型的转换
/// </summary>
/// <param name="buffer"></param>
/// <param name="offset"></param>
/// <param name="bitOffset"></param>
/// <param name="s7DataType"></param>
/// <param name="array"></param>
/// <returns></returns>
/// <exception cref="ArgumentException"></exception>
    private static string TransferBufferToData(byte[] buffer, int offset, byte? bitOffset, S7DataTypeEnum s7DataType,byte? array)
    {
     var result = string.Empty;
     switch (s7DataType)
     {
         case S7DataTypeEnum.Bool:
         {
             var res = buffer.Skip(offset).Take(1).ToArray();
             result = TransferBufferHelper.ByteFromBool(res[0], bitOffset.Value).ToString();
             break;
         }
         case S7DataTypeEnum.Byte:
         {
             var res = buffer.Skip(offset).Take(1).ToArray();
             result = res[0].ToString();
             break;
         }
         case S7DataTypeEnum.Int:
         {
             var res = buffer.Skip(offset).Take(2).ToArray();
             result = TransferBufferHelper.IntFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.DInt:
         {
             var res = buffer.Skip(offset).Take(4).ToArray();
             result = TransferBufferHelper.DIntFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.Word:
         {
             var res = buffer.Skip(offset).Take(2).ToArray();
             result = TransferBufferHelper.WordFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.DWord:
         {
             var res = buffer.Skip(offset).Take(4).ToArray();
             result = TransferBufferHelper.DWordFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.Real:
         {
             var res = buffer.Skip(offset).Take(4).ToArray();
             result = TransferBufferHelper.RealFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.LReal:
         {
             var res = buffer.Skip(offset).Take(8).ToArray();
             result = TransferBufferHelper.LRealFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.String:
         {
             var res = buffer.Skip(offset).Take(array.Value).ToArray();
             result = TransferBufferHelper.S7StringFromByteArray(res, Encoding.ASCII);
             break;
         }
         case S7DataTypeEnum.S7String:
         {
             var res = buffer.Skip(offset).Take(array.Value).ToArray();
             result = TransferBufferHelper.S7StringFromByteArray(res, Encoding.ASCII);
             break;
         }
         case S7DataTypeEnum.Array:
         {
             var res = buffer.Skip(offset).Take(array.Value).ToArray();
             var hexValues = Array.ConvertAll(res, b => b.ToString());
             result = string.Join(',', hexValues);
             break;
         }
         default:
            throw new ArgumentException("没有该类型注入");
     }

     return result;
 }

  
}