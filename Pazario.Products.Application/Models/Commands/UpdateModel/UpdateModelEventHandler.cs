using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Models.Events;

namespace Pazario.Products.Application.Models.Commands.UpdateModel
{
    public class UpdateModelEventHandler (ICacheService cacheService): INotificationHandler<UpdateModelEvent>
    {
        public async Task Handle(UpdateModelEvent notification, CancellationToken cancellationToken)
        {
            await cacheService.RemoveAsync(CacheKeys.ModelsCacheKey);
        }
    }
}
