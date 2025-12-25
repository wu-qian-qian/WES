using Common.Application.MediatR.Messaging;
using Common.Domain;

namespace S7.Application.Events.WriteBuffer;

public class WriteBufferCommandHandler : ICommandHandler<WriteBufferCommand>
{
    public Task<Result> Handle(WriteBufferCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}