using Common.Application.EventBus;
using Common.Domain;

namespace S7.CustomEvents;

public class UpdateEntityCacheEvent:IIntegrationEvent<Result>
{   
  public bool LoadNe{get;set;}
}
