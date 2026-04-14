using MediatR;
using Pazario.Common.Domain.Abstractions;

namespace Pazario.Common.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
