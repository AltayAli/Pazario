using Pazario.Common.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;

namespace Pazario.Products.Application.CategoryProperties.Queries.GetCategoryProperties
{
    public class GetCategoryPropertiesQuery : ICacheQuery<List<GetCategoryProperiesResponse>>
    {
        public string Key { get; set; }
        public Guid CategoryId { get; set; }

        public string CacheKey => CacheKeys.CategoryPropertiesCacheKey;

        public TimeSpan? Expiration => TimeSpan.FromMinutes(40);
    }
}
