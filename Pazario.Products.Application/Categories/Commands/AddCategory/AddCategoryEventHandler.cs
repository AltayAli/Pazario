using MediatR;
using Pazario.Common.Application.Caching;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Categories.Events;

namespace Pazario.Products.Application.Categories.Commands.AddCategory
{
    public class AddCategoryEventHandler 
        (ICacheService cacheService)
        : INotificationHandler<AddCategoryEvent>
    {
        public async Task Handle(AddCategoryEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.MarkasCacheKey, cancellationToken);
        }
    }
}
