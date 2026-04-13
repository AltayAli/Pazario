using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;

namespace Pazario.Products.Application.Products.Queries.GetProducts
{
    public record GetProductsQuery : ICacheQuery<List<GetProductsItemResponse>>
    {
        public string Key { get; set; } = string.Empty;

        public string CacheKey => CacheKeys.ProductsCacheKey;
        public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
    }
}
