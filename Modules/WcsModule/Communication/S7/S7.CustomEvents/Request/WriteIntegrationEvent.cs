using Common.Application.EventBus;
using Common.Domain;

namespace S7.CustomEvents;

public class WriteIntegrationEvent:IIntegrationEvent<Result>
{
    public string DeviceName { get; set; }

    public IReadOnlyDictionary<string,string> DBNameToDataValue { get; set; }
}
