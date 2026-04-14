using MediatR;
using Pazario.Common.Domain.Abstractions;

namespace Pazario.Common.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
    {
    }
}
