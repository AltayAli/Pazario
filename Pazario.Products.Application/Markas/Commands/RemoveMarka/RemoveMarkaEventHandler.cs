using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Markas.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Commands.RemoveMarka
{
    public class RemoveMarkaEventHandler
        (ICacheService cacheService)
        : INotificationHandler<RemoveMarkaEvent>
    {
        public async Task Handle(RemoveMarkaEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.MarkasCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.MarkasCacheKeyPlus(notification.Id), cancellationToken);
        }
    }
}
