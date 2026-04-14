using MediatR;
using Pazario.Common.Application.Caching;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Markas.Events;

namespace Pazario.Products.Application.Markas.Commands.UpdateMarka
{
    public class UpdateMarkaEventHandler
        (ICacheService cacheService) 
        : INotificationHandler<UpdateMarkaEvent>
    {
        public async Task Handle(UpdateMarkaEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.MarkasCacheKey, cancellationToken);
        }
    }
}
