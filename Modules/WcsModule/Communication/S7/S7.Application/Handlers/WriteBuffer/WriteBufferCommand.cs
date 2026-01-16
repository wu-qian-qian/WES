using Common.Application.MediatR.Messaging;

namespace S7.Application.Events.WriteBuffer;

public class WriteBufferCommand : ICommand
{
    public string DeviceName { get; set; }

    public Dictionary<string, string> DBNameToDataValue { get; set; }
}