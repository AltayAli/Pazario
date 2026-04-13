using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Products.Events;

namespace Pazario.Products.Application.Products.Commands.CreateProduct
{
    public class CreateProductEventHandler(ICacheService cacheService)
        : INotificationHandler<ProductCreateEvent>
    {
        public async Task Handle(ProductCreateEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ProductsCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ProductCacheKey(notification.Id), cancellationToken);
        }
    }
}
