using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;

namespace Pazario.Products.Application.Markas.Queries.GetMarkas
{
    public record GetMarkasQuery : ICacheQuery<List<GetMarkasResponse>>
    {
        public string Key { get; set; }

        public string CacheKey => CacheKeys.MarkasCacheKey;

        public TimeSpan? Expiration => TimeSpan.FromDays(1);
    }
}
