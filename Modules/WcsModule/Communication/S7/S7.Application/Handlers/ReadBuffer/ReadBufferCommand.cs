using Common.Application.MediatR.Messaging;

namespace S7.Application.Handlers.ReadBuffer;

public class ReadBufferCommand : ICommand
{
    public string DeviceName { get; set; }
}