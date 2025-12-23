using Common.Application.MediatR.Messaging;

namespace S7.Application.Events.ReadBuffer;

public class ReadBufferCommand:ICommand
{
    public string DeviceName { get; set; }
    
    public string ReadKey { get; set; }
    
    public string EventKey { get; set; }
}