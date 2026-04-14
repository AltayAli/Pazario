using MediatR;
using Pazario.Common.Application.Caching;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Categories.Events;

namespace Pazario.Products.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryEventHandler (ICacheService cacheService): INotificationHandler<UpdateCategoryEvent>
    {
        public async Task Handle(UpdateCategoryEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.CategoriesCacheKey, cancellationToken);
        }
    }
}
