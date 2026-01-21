using Common.Application.MediatR.Messaging;
using Common.Application.NetWork.Other.Base;
using Common.Domain;
using Common.Helper;
using S7.Application.Abstractions.Data;
using S7.Application.Services;
using S7.Domain.Attributes;
using S7.Domain.Repository;

namespace S7.Application.Handlers;

public class ReadBufferCommandHandler(INetService _netService,IPlcEntityRepository _plcEntityRepository
,IS7AnalysisService _analysisService)
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
            var plcEntity = (await _plcEntityRepository.GetQueryableAsync())
            .Where(p => p.DeviceName == request.DeviceName && p.IsUse).ToArray();
            //按照 ip 和 db 快进行区分
        var plcEntityGroups = plcEntity.GroupBy(p => new { p.Ip, p.DBAddress, p.S7BlockType });
        List<EntityModel> entityModels=new List<EntityModel>();
        foreach (var plcEntityGroup in plcEntityGroups)
        {
            var read = new ReadModel();
            read.Ip = plcEntityGroup.Key.Ip;
            read.DBAddress = plcEntityGroup.Key.DBAddress;
            read.S7BlockType = plcEntityGroup.Key.S7BlockType;
            read.DBStart = plcEntityGroup.MinBy(p => p.Index).DataOffset;
            var dataType = plcEntityGroup.MaxBy(p => p.Index)?.S7DataType;
            var index = dataType?.GetEnumAttribute<S7DataTypeAttribute>()?.DataSize;
            read.DBCount = plcEntityGroup.MaxBy(p => p.Index).DataOffset + index.Value;
            var readResult = await _netService.ReadAsync(read);
            if (readResult.IsSuccess)
            {
                entityModels.AddRange(await _analysisService.AnalysisAsync(readResult.Value,plcEntityGroup.ToArray()));
            }
            else
            {
                Result.Error<IEnumerable<EntityModel>>(readResult.Message);
            }

        }
        if (entityModels.Count > 0&&result==null)
        {
            result=Result.Success<IEnumerable<EntityModel>>(entityModels);
        }
        return result ?? Result.Error<IEnumerable<EntityModel>>("读取失败");
    }

}