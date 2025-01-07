using MediatR;
using TradeUp.Domain.Core.Abstractions;

namespace Bookify.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
