using Microsoft.Extensions.Caching.Distributed;

namespace Pazario.Products.Infrastructure.Caching
{
    public static class CacheOptions
    {
        public static DistributedCacheEntryOptions DefaultExpiration = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
        };

        public static DistributedCacheEntryOptions Create(TimeSpan? expiration = null)
        {
            return expiration is null ?
                DefaultExpiration :
                new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = expiration
                };
        }
    }
}
