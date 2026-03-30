using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Products.Events;

namespace Pazario.Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductEventHandler 
        (ICacheService cacheService)
        : INotificationHandler<ProductUpdateEvent>
    {
        public async Task Handle(ProductUpdateEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);    
        }
    }
}
