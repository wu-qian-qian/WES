using Common.Domain;
using MediatR;

namespace Common.Application.MediatR.Messaging;

/// <summary>
///     查询实体
/// </summary>
/// <typeparam name="TResponse">返回实体</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;