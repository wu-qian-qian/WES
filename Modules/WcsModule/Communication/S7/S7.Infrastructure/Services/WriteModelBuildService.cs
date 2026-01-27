using System.Collections.Concurrent;
using S7.Application.Abstractions.Data;
using S7.Application.Handlers;
using S7.Application.Services;
using S7.Domain.Entities;


namespace S7.Infrastructure.Service;

public class WriteModelBuildService : IWriteModelBuildService
{
    private static ConcurrentDictionary<string,IEnumerable<CacheEntityModel>> _writeModeMap=new();
    public Task<byte[]> DataTranferBuffer(string data)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<S7WriteModel>> PlcWriteModelBuildAsync(string deviceName, 
    IReadOnlyDictionary<string, string> keyValues)
    {
        throw new NotImplementedException();
    }

    public Task LoadAsync(IEnumerable<PlcEntityItem> plcEntityItems)
    {
        var entityGroup=plcEntityItems.GroupBy(p=>p.DeviceName);
        _writeModeMap.Clear();
        foreach(var item in entityGroup)
        {
            
        }
    }
}