using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryPropertyValues;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Products;
using Pazario.Products.Domain.ProductVariantImages;
using Pazario.Products.Domain.ProductVariantProperties;

namespace Pazario.Products.Domain.ProductVariants
{
    public class ProductVariant : BaseEntity
    {
        private ProductVariant()
        {
            CategoryPropertyValues = new HashSet<CategoryPropertyValue>();
            Images = new HashSet<ProductVariantImage>();
            Properties = new HashSet<ProductVariantProperty>();
        }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }
        public Sku Sku { get; private set; }
        public Barcode Barcode { get; private set; }
        public Money Price { get; private set; }
        public Money? Cost { get; private set; }
        public decimal? TaxRate { get; private set; }
        public Stock Stock { get; private set; }
        public bool IsDefault { get; private set; }
        public bool IsActive { get; private set; }
        public Money DiscountCount { get; private set; }
        public DateTime? DiscountStartDate { get; private set; }
        public DateTime? DiscountEndDate { get; private set; }
        public HashSet<ProductVariantImage> Images { get; private set; }
        public HashSet<CategoryPropertyValue> CategoryPropertyValues { get; private set; }
        public HashSet<ProductVariantProperty> Properties { get; private set; }
    }
}
