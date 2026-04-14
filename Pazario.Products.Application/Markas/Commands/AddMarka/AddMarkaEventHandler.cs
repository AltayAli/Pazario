using MediatR;
using Pazario.Common.Application.Caching;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Markas.Events;

namespace Pazario.Products.Application.Markas.Commands.AddMarka
{
    public class AddMarkaEventHandler
        (ICacheService cacheService)
        : INotificationHandler<AddMarkaEvent>
    {
        public async Task Handle(AddMarkaEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.MarkasCacheKey,cancellationToken);

        }
    }
}
