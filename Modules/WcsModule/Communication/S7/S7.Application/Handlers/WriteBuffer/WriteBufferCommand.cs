using Common.Application.MediatR.Messaging;

namespace S7.Application.Handlers.WriteBuffer;

public class WriteBufferCommand : ICommand
{
    public string DeviceName { get; set; }

    public IReadOnlyDictionary<string,string> DBNameToDataValue { get; set; }
}