using Common.Application.MediatR.Messaging;

namespace S7.Application.Handlers.UpdateCache;
public class UpdateCacheCommand : ICommand
{
   public bool LoadNet{get;set;}
}