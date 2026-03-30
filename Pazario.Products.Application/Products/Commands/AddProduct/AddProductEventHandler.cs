using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Products.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Products.Commands.AddProduct
{
    public class AddProductEventHandler 
        (ICacheService cacheService)
        : INotificationHandler<ProductCreateEvent>
    {
        public async Task Handle(ProductCreateEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey,cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);
        }
    }
}
