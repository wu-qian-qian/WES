using Common.Application.EventBus;
using Common.Domain;

namespace S7.CustomEvents;

public class ReadIntegrationEvent:IIntegrationEvent<Result<IEnumerable<ReadModel>>>
{   
    public string DeviceName{get;set;}
}
 