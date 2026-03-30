using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Categories.Events;

namespace Pazario.Products.Application.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryEventHandler(ICacheService cacheService) : INotificationHandler<RemoveCategoryEvent>
    {
        public async Task Handle(RemoveCategoryEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.CategoriesCacheKey, cancellationToken);
        }
    }
}
