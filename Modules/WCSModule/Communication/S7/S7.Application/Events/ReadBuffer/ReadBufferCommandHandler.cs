using Common.Application.MediatR.Messaging;
using Common.Domain;

namespace S7.Application.Events.ReadBuffer;

public class ReadBufferCommandHandler:ICommandHandler<ReadBufferCommand>
{
    public  Task<Result> Handle(ReadBufferCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}