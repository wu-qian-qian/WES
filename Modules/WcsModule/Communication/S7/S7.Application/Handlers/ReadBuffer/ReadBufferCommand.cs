using Common.Application.MediatR.Messaging;

namespace S7.Application.Handlers;

public class ReadBufferCommand : ICommand<IEnumerable<EntityModel>>
{
    public string DeviceName { get; set; }
}