using S7.Application.Handlers;
using S7.Domain.Entities;

namespace S7.Application.Services;
public interface IS7AnalysisService
{
    Task<IEnumerable<EntityModel>> AnalysisAsync(byte[] buffer,IEnumerable<PlcEntityItem> plcEntityItems);
}