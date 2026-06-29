using System.Collections.Concurrent;
using S7.Application;
using S7.Application.Abstractions.Data;
using S7.Application.Services;
using S7.Domain.Entities;
using S7.Infrastructure.Helper;


namespace S7.Infrastructure.Service;

public class WriteModelBuildService : IWriteModelBuildService
{
    private static ConcurrentDictionary<string, IReadOnlyList<WriteCacheEntityModel>> _writeModeMap = new(StringComparer.OrdinalIgnoreCase);

    public Task<IEnumerable<S7WriteModel>> PlcWriteModelBuildAsync(string deviceName, 
    IReadOnlyDictionary<string, string> keyValues)
    {
        if (string.IsNullOrWhiteSpace(deviceName) || keyValues == null || keyValues.Count == 0)
            return Task.FromResult<IEnumerable<S7WriteModel>>(Array.Empty<S7WriteModel>());

        if (!_writeModeMap.TryGetValue(deviceName, out var deviceCacheMode) || deviceCacheMode.Count == 0)
            throw new KeyNotFoundException($"未获取到设备 {deviceName} 的缓存变量数据");

        List<S7WriteModel> writeModels = new();
        foreach (var item in keyValues)
        {
            var cacheEntity = deviceCacheMode.FirstOrDefault(p => string.Equals(p.DBName, item.Key, StringComparison.OrdinalIgnoreCase));
            if (cacheEntity.DBName == null)
                throw new KeyNotFoundException($"未找到写入缓存变量: {item.Key}");

            S7WriteModel writeModel = new()
            {
                BitAddress = cacheEntity.BitOffset,
                DBAddress = cacheEntity.DBAddress,
                IsBit = cacheEntity.S7DataType == Domain.Enums.S7DataTypeEnum.Bool,
                Ip = cacheEntity.Ip,
                S7BlockType = cacheEntity.S7BlockType,
                DBStart = cacheEntity.DataOffset
            };
            if (writeModel.IsBit == true)
            {
                writeModel.BitValue = bool.Parse(item.Value);
            }
            else
            {
                writeModel.Buffer = TransferHelper.TransferDataToBuffer(item.Value, cacheEntity.S7DataType, cacheEntity.ArrayLen);
            }
            writeModels.Add(writeModel);
        }
        return Task.FromResult<IEnumerable<S7WriteModel>>(writeModels);
    }

    public Task LoadAsync(IEnumerable<PlcEntityItem> plcEntityItems)
    {
        var writeModeMap = new ConcurrentDictionary<string, IReadOnlyList<WriteCacheEntityModel>>(StringComparer.OrdinalIgnoreCase);
        foreach (var item in (plcEntityItems ?? Array.Empty<PlcEntityItem>()).Where(p => p != null).GroupBy(p => p.DeviceName))
        {
            var cacheEntityModels = item.OrderBy(p => p.DataOffset)
                .Select(p => new WriteCacheEntityModel
                {
                    DataOffset = p.DataOffset,
                    DBName = p.Name,
                    BitOffset = p.BitOffset,
                    DBAddress = p.DBAddress,
                    S7DataType = p.S7DataType,
                    Ip = p.Ip,
                    S7BlockType = p.S7BlockType,
                    ArrayLen = p.ArrayLength ?? 0
                })
                .ToArray();
            writeModeMap[item.Key] = cacheEntityModels;
        }

        _writeModeMap = writeModeMap;
        return Task.CompletedTask;
    }
}