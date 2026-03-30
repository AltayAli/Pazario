using MediatR;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Categories.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
