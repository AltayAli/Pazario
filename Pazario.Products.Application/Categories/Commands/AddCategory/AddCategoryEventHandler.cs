using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Categories.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
