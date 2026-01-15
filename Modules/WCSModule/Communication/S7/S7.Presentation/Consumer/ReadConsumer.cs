using MassTransit;
using MediatR;
using S7.ConsumerEvent;
namespace S7.Presentation;

public class ReadConsumer(ISender _sender): IConsumer<ReadMessage>
{
    public Task Consume(ConsumeContext<ReadMessage> context)
    {
        throw new NotImplementedException();
    }
}
