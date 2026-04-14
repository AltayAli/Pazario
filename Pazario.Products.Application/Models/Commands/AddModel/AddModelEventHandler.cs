using MediatR;
using Pazario.Common.Application.Caching;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Models.Events;

namespace Pazario.Products.Application.Models.Commands.AddModel
{
    internal class AddModelEventHandler (ICacheService cacheService) : INotificationHandler<AddModelEvent>
    {
        public async Task Handle(AddModelEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.MarkasCacheKey, cancellationToken);
            await cacheService.RemoveAsync(CacheKeys.ModelsCacheKey, cancellationToken);
        }
    }
}
