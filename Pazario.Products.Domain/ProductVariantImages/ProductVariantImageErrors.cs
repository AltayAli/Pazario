using Pazario.Common.Domain.Abstractions;

namespace Pazario.Products.Domain.ProductVariantImages
{
    public class ProductVariantImageErrors
    {
        public static Error NullValue => new Error("ProductVariantImage.NullValue", "ProductVariantImage.NullValue");
        public static Error NotFound => new Error("ProductVariantImage.NotFound", "ProductVariantImage.NotFound");
    }
}
