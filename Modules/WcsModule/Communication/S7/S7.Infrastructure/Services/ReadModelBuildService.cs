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
    private static ConcurrentDictionary<string, IReadOnlyList<S7ReadModel>> _readModeMap = new(StringComparer.OrdinalIgnoreCase);
    /// <summary>
    /// 用来缓存 变量实体
    /// 读取解析缓存
    /// </summary>
    private static ConcurrentDictionary<string, IReadOnlyList<ReadCacheEntityModel>> _readEntityMap = new(StringComparer.OrdinalIgnoreCase);
    /// <summary>
    /// 将buffer数据转换为字符数据
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="key">设备名+数据块位置+数块类型</param>
    /// <returns></returns>
    public Task<IEnumerable<EntityModel>> ReadEntityModelBuildAsync(byte[] buffer, string key)
    {
        if (buffer == null || buffer.Length == 0 || string.IsNullOrWhiteSpace(key))
            return Task.FromResult<IEnumerable<EntityModel>>(Array.Empty<EntityModel>());

        if (!_readEntityMap.TryGetValue(key, out var entityModels) || entityModels.Count == 0)
            return Task.FromResult<IEnumerable<EntityModel>>(Array.Empty<EntityModel>());

        var tempModel = entityModels.Select(p =>
        {
            EntityModel model = new();
            model.DBName = p.DBName;
            model.DBValue = TransferHelper.TransferBufferToData(buffer, p.DataOffset, p.BitOffset, p.S7DataType, p.ArrayLength);
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
        if (string.IsNullOrWhiteSpace(deviceName))
            return Task.FromResult<IEnumerable<S7ReadModel>>(Array.Empty<S7ReadModel>());

        return _readModeMap.TryGetValue(deviceName, out var models) && models.Count > 0
            ? Task.FromResult<IEnumerable<S7ReadModel>>(models.ToArray())
            : Task.FromResult<IEnumerable<S7ReadModel>>(Array.Empty<S7ReadModel>());
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
        var readModeMap = new ConcurrentDictionary<string, IReadOnlyList<S7ReadModel>>(StringComparer.OrdinalIgnoreCase);
        var readEntityMap = new ConcurrentDictionary<string, IReadOnlyList<ReadCacheEntityModel>>(StringComparer.OrdinalIgnoreCase);

        foreach (var plcEntityGroup in (plcEntityList ?? Array.Empty<PlcEntityItem>()).Where(p => p != null)
                     .GroupBy(p => new { p.DeviceName, p.Ip, p.DBAddress, p.S7BlockType }))
        {
            var read = new S7ReadModel();
            read.Ip = plcEntityGroup.Key.Ip;
            read.DBAddress = plcEntityGroup.Key.DBAddress;
            read.S7BlockType = plcEntityGroup.Key.S7BlockType;

            var orderedEntities = plcEntityGroup.OrderBy(p => p.DataOffset).ToArray();
            var minOffset = orderedEntities.Min(p => p.DataOffset);
            var maxOffset = orderedEntities.Max(p => p.DataOffset);
            var dataType = orderedEntities.LastOrDefault(p => p.DataOffset == maxOffset)?.S7DataType;
            var dataSize = dataType?.GetEnumAttribute<S7DataTypeAttribute>()?.DataSize ?? 0;

            read.DBStart = minOffset;
            read.DBCount = Math.Max(0, maxOffset + dataSize - minOffset);

            var deviceName = plcEntityGroup.Key.DeviceName;
            var nodeList = readModeMap.TryGetValue(deviceName, out var existingNodes)
                ? existingNodes.ToList()
                : new List<S7ReadModel>();
            // 添加新设备的读取
            nodeList.Add(read);
            readModeMap[deviceName] = nodeList.ToArray();
            //同一块的数据 变量进行缓存解析；缓存映射
            var key = string.Join("|", deviceName, plcEntityGroup.Key.DBAddress, plcEntityGroup.Key.Ip, plcEntityGroup.Key.S7BlockType);
            var entityModels = orderedEntities.Select(p => new ReadCacheEntityModel
            {
                DataOffset = p.DataOffset,
                DBName = p.Name,
                BitOffset = p.BitOffset,
                ArrayLength = p.ArrayLength,
                S7DataType = p.S7DataType,
            }).ToArray();
            readEntityMap[key] = entityModels;
        }

        _readModeMap = readModeMap;
        _readEntityMap = readEntityMap;
        return Task.CompletedTask;
    }
}