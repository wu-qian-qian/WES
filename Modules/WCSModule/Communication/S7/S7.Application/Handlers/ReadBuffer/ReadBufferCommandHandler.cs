using System.ComponentModel.DataAnnotations;
using Common.Application.MediatR.Messaging;
using Common.Application.NET.Other.Base;
using Common.Domain;
using Common.Helper;
using S7.Application.Abstractions.Data;
using S7.Domain;
using S7.Domain.Attributes;

namespace S7.Application.Events.ReadBuffer;

public class ReadBufferCommandHandler (INetService _netService
,IPlcEntityRepository _plcEntityRepository): ICommandHandler<ReadBufferCommand>
{
    public async Task<Result> Handle(ReadBufferCommand request, CancellationToken cancellationToken)
    {
        Result? result=default;
        var plcEntity=(await _plcEntityRepository.GetQueryableAsync())
        .Where(p=>p.DeviceName==request.DeviceName&&p.IsUse).ToArray();
        //按照 ip 和 db 快进行区分
        var plcEntityGroups=plcEntity.GroupBy(p=>new {p.Ip,p.DBAddress,p.S7BlockType});
        foreach(var plcEntityGroup in plcEntityGroups)
        {
            ReadModel read=new ReadModel();
            read.Ip=plcEntityGroup.Key.Ip;
            read.DBAddress=plcEntityGroup.Key.DBAddress;
            read.S7BlockType=plcEntityGroup.Key.S7BlockType;
            read.DBStart= plcEntityGroup.MinBy(p=>p.Index).DataOffset;
            var dataType= plcEntityGroup.MaxBy(p=>p.Index).S7DataType;
            var index=dataType.GetEnumAttribute<S7DataTypeAttribute>().DataSize;
            read.DBCount= plcEntityGroup.MaxBy(p=>p.Index).DataOffset+index;
            var readResult= await _netService.ReadAsync(read);
        }
        return result??Result.Error("读取失败");
    }
}