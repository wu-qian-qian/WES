using System.Collections.Concurrent;
using Common.Helper;
using S7.Application;
using S7.Application.Abstractions.Data;
using S7.Application.Handlers;
using S7.Application.Services;
using S7.Domain.Attributes;
using S7.Domain.Entities;
using S7.Infrastructure.Helper;


namespace S7.Infrastructure.Service;

public class ReadModelBuildService : IReadModelBuildService
{
    /// <summary>
    /// 用来缓存 读取db块的数据模型
    /// 设备名+DB块地址+IP+数据块类型
    /// </summary>
    private static ConcurrentDictionary<string,IEnumerable<S7ReadModel>> _readModeMap=new();
    /// <summary>
    /// 用来缓存 变量实体
    /// </summary>
     private static ConcurrentDictionary<string,IEnumerable<ReadCacheEntityModel>> _readEntityMap=new();
    /// <summary>
    /// 将buffer数据转换为字符数据
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="key">设备名+数据块位置+数块类型</param>
    /// <returns></returns>
    public Task<IEnumerable<EntityModel>> ReadEntityModelBuildAsync(byte[] buffer, string key)
    {
        IEnumerable<ReadCacheEntityModel> entityModels=_readEntityMap[key];
        var tempModel= entityModels.Select(p =>
        {
            EntityModel model=new EntityModel();
            model.DBName=p.DBName;
            model.DBValue=TransferHelper.TransferBufferToData(buffer,p.DataOffset,p.BitOffset,p.S7DataType,p.ArrayLength);
           return model; 
        });
        return Task.FromResult(tempModel);
    } 
/// <summary>
/// 构建读取体
/// </summary>
/// <param name="deviceName"></param>
/// <returns></returns>
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
            //用来组装读取模型
            tempNodeList.Add(read);
            _readModeMap.TryAdd(plcEntityGroup.Key.DeviceName,tempNodeList.ToArray());
            //数据模型组装   主要用来缓存解析数据模型
            var key=plcEntityGroup.Key.DeviceName+plcEntityGroup.Key.DBAddress+plcEntityGroup.Key.Ip
            +plcEntityGroup.Key.S7BlockType;
             var entityModels=plcEntityGroup.ToList()
             .Select(p =>
             {
                return new ReadCacheEntityModel()
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
}