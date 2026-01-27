using S7.Application.Abstractions.Data;
using S7.Domain.Entities;

namespace S7.Application.Services;

public interface IWriteModelBuildService
{
    Task<IEnumerable<S7WriteModel>> PlcWriteModelBuildAsync(string deviceName,IReadOnlyDictionary<string,string> keyValues);

/// <summary>
/// 緩存加載
/// 可使用定時刷 
/// 程序啓動時刷
/// </summary>
/// <param name="plcEntityItems"></param>
/// <returns></returns>
    Task LoadAsync(IEnumerable<PlcEntityItem> plcEntityItems);
}