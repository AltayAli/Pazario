using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;

namespace Pazario.Products.Application.Markas.Queries.GetMarka
{
    public class GetMarkaQuery : ICacheQuery<GetMarkaResponse>
    {
        public Guid Id { get; set; }

        public string CacheKey => CacheKeys.MarkaCacheKey(Id);

        public TimeSpan? Expiration => TimeSpan.FromDays(1);
    }
}
