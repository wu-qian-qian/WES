using Common.Application.MediatR.Messaging;
using Common.Application.NetWork.Other.Base;
using Common.Domain;
using Common.Helper;
using S7.Application.Abstractions.Data;
using S7.Application.Services;
using S7.Domain.Attributes;
using S7.Domain.Repository;

namespace S7.Application.Handlers;

/// <summary>
/// 可以进行拆分 
/// 添加 2个事件用来读取 和 解析
/// 该事件只做db访问处理 
/// </summary>
/// <param name="_netService"></param>
/// <param name="_plcEntityRepository"></param>
/// <param name="_modelService"></param>
public class ReadBufferCommandHandler(INetService _netService
,IReadModelBuildService _modelService)
    : ICommandHandler<ReadBufferCommand,IEnumerable<EntityModel>>
{
    /// <summary>
    /// 变量缓存
    /// 方式一启用内存缓存减少数据库访问 创建一个线程安全字典
    /// 方式二直接读取数据库实时性高
    /// 方式三放入缓存服务一定时间进行更新
    /// 
    /// 每一种方式一个事件
    /// </summary>
    public async Task<Result<IEnumerable<EntityModel>>> Handle(ReadBufferCommand request, CancellationToken cancellationToken)
    {
        Result<IEnumerable<EntityModel>>? result = default;
        string msg=string.Empty;
        List<EntityModel> entityModels=new List<EntityModel>();
        var readModels=await _modelService.ReadPlcModelBuildAsync(request.DeviceName);
        foreach(var item in readModels)
        {
            var tempResult=await _netService.ReadAsync(item);
            if (tempResult.IsSuccess == false)
            {
                entityModels.Clear();
                msg=tempResult.Message;
                break;
            }
            string key=request.DeviceName+item.DBAddress+item.Ip+item.S7BlockType;
            entityModels.AddRange(await _modelService.ReadEntityModelBuildAsync(tempResult.Value,key));
        }
        if (entityModels.Count > 0)
        {
            result=Result.Success<IEnumerable<EntityModel>>(entityModels);
        }
        return result ?? Result.Error<IEnumerable<EntityModel>>($"读取失败--{msg}");
    }

}