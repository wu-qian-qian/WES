using MassTransit;
using MediatR;
using S7.ConsumerEvent;
namespace S7.Presentation;

public class ReadConsumerHandler(ISender _sender): IConsumer<ReadConsumerEvent>
{
    public Task Consume(ConsumeContext<ReadConsumerEvent> context)
    {
        throw new NotImplementedException();
    }
}
