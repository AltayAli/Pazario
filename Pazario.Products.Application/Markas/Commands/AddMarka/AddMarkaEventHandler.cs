using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Application.Markas.Queries.GetMarka;
using Pazario.Products.Domain.Markas;
using Pazario.Products.Domain.Markas.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
