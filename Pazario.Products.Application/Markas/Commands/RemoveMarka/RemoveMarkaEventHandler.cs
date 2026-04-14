using MediatR;
using Pazario.Common.Application.Caching;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Markas.Events;

namespace Pazario.Products.Application.Markas.Commands.RemoveMarka
{
    public class RemoveMarkaEventHandler
        (ICacheService cacheService)
        : INotificationHandler<RemoveMarkaEvent>
    {
        public async Task Handle(RemoveMarkaEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.MarkasCacheKey, cancellationToken);
        }
    }
}
