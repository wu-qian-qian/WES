using MassTransit;

public class WriteConsumer: IConsumer<WriteMessage>
{
    public Task Consume(ConsumeContext<WriteMessage> context)
    {
        throw new NotImplementedException();
    }
}