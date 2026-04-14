using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;

namespace Pazario.Products.Application.Products.Queries.GetProduct
{
    public record GetProductQuery : ICacheQuery<GetProductResponse>
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeys.ProductCacheKey(Id);
        public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
    }
}
