using Pazario.Products.Domain.Abstractions;

namespace Pazario.Products.Domain.ProductVariantProperties
{
    public class ProductVariantPropertyErrors
    {
        public static Error NullValue => new Error("ProductVariantProperty.NullValue", "ProductVariantProperty.NullValue");
        public static Error NotFound => new Error("ProductVariantProperty.NotFound", "ProductVariantProperty.NotFound");
    }
}
