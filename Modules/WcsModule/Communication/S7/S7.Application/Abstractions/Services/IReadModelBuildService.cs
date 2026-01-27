using S7.Application.Abstractions.Data;
using S7.Application.Handlers;
using S7.Domain.Entities;
using S7.Domain.Repository;

namespace S7.Application.Services;

/// <summary>
/// 读取模型构建器
/// 主要用来构建PLC 读取的配置
/// plc 数据转换
/// </summary>
public interface IReadModelBuildService
{
    /// <summary>
    /// 数据解析
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="key">ip+数据块位置+数据块类型</param>
    /// <returns></returns>
    Task<IEnumerable<EntityModel>> ReadEntityModelBuildAsync(byte[] buffer, string key);
/// <summary>
/// 读取模型的获取
/// </summary>
/// <param name="deviceName"></param>
/// <returns></returns>
    Task<IEnumerable<S7ReadModel>> ReadPlcModelBuildAsync(string deviceName);

/// <summary>
/// 初始化
/// </summary>
/// <param name="_plcEntityRepository"></param>
/// <returns></returns>
    Task LoadAsync(IEnumerable<PlcEntityItem> plcEntityList);
}