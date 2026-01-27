using S7.Application.Abstractions.Data;
using S7.Domain.Entities;

namespace S7.Application.Services;

public interface IWriteModelBuildService
{
    Task<IEnumerable<S7WriteModel>> PlcWriteModelBuildAsync(string deviceName,IReadOnlyDictionary<string,string> keyValues);

    Task<byte[]> DataTranferBuffer(string data);

    Task LoadAsync(IEnumerable<PlcEntityItem> plcEntityItems);
}