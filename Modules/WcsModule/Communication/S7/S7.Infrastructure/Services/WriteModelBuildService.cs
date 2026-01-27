using System.Collections.Concurrent;
using S7.Application;
using S7.Application.Abstractions.Data;
using S7.Application.Services;
using S7.Domain.Entities;
using S7.Infrastructure.Helper;


namespace S7.Infrastructure.Service;

public class WriteModelBuildService : IWriteModelBuildService
{
    private static ConcurrentDictionary<string,IEnumerable<WriteCacheEntityModel>> _writeModeMap=new();

    public Task<IEnumerable<S7WriteModel>> PlcWriteModelBuildAsync(string deviceName, 
    IReadOnlyDictionary<string, string> keyValues)
    {
        List<S7WriteModel> writeModels=new List<S7WriteModel>();
        _writeModeMap.TryGetValue(deviceName,out var deviceCacheMode);
        if(deviceCacheMode==null)
         throw new ArgumentNullException("未获取到设备的缓存变量数据");
        foreach(var item in keyValues)
        {
            var cacheEntity=deviceCacheMode.First(p=>p.DBName==item.Key);
             S7WriteModel writeModel=new S7WriteModel
             {
               BitAddress=cacheEntity.BitOffset,
               DBAddress=cacheEntity.DBAddress,
               IsBit=cacheEntity.S7DataType==Domain.Enums.S7DataTypeEnum.Bool,
               Ip=cacheEntity.Ip,
               S7BlockType=cacheEntity.S7BlockType,
                DBStart=cacheEntity.DataOffset
             };
            if (writeModel.IsBit == true)
            {
                writeModel.BitValue=bool.Parse(item.Value);
            }
            else
            {
                writeModel.Buffer=TransferHelper.TransferDataToBuffer(item.Value,cacheEntity.S7DataType,cacheEntity.ArrayLen);
            }
            writeModels.Add(writeModel);
        }
     return Task.FromResult<IEnumerable<S7WriteModel>>(writeModels);
    }

    public Task LoadAsync(IEnumerable<PlcEntityItem> plcEntityItems)
    {
        var entityGroup=plcEntityItems.GroupBy(p=>p.DeviceName);
        _writeModeMap.Clear();
        foreach(var item in entityGroup)
        {
             var cacheEntityModels=item.ToArray()
             .Select(p =>
             {
                return new WriteCacheEntityModel()
                {
                    DataOffset=p.DataOffset,
                    DBName=p.Name,
                    BitOffset=p.BitOffset,
                    DBAddress=p.DBAddress,
                    S7DataType=p.S7DataType,
                    Ip=p.Ip,
                    S7BlockType=p.S7BlockType,
                    ArrayLen=p.ArrayLength.Value
                };
             });
             _writeModeMap.TryAdd(item.Key,cacheEntityModels);
        }
        return Task.CompletedTask;
    }
}