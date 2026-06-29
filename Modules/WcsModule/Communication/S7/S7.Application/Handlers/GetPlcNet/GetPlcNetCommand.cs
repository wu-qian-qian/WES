using Common.Application.MediatR.Messaging;
using S7.Application.Abstractions.Data;

namespace S7.Application.Handlers.GetPlcNet;

public class GetPlcNetQuery : IQuery<IEnumerable<S7NetModel>>
{
}