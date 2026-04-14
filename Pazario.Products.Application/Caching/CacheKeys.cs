namespace Pazario.Products.Application.Caching
{
    public static class CacheKeys
    {
        public static string ProductsCacheKey => "products-cache-key";
        public static string MarkasCacheKey => "markas-cache-key";
        public static string ModelsCacheKey => "models-cache-key";
        public static string CategoriesCacheKey => "categories-cache-key";
        public static string CategoryPropertiesCacheKey => "category-properties-cache-key";

        public static string MarkaCacheKey(Guid id) => $"marka-cache-key-{id}";
        public static string ProductCacheKey(Guid id) => $"product-cache-key-{id}";
    }
}
