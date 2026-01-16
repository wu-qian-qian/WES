using Common.Application.Log;
using Common.Application.MediatR.Messaging;
using MediatR;

namespace Common.Application.MediatR.Behaviors;

public class BasicPipelineBehavior<TRequest, TResponse>(ILogService logService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested)
        {
        }

        return await next();
    }
}