using Pazario.Common.Domain.Abstractions;

namespace Pazario.Products.Domain.Products
{
    public class ProductErrors
    {
        public static Error NullValue => new Error("Product.NullValue", "Product.NullValue");
        public static Error NotFound => new Error("Product.NotFound", "Product.NotFound");
        public static Error MaxLenght => new Error("Product.MaxLenght", "Product.MaxLenght");
    }
}
