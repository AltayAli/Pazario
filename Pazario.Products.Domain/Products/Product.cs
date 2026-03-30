using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.ProductCategories;
using Pazario.Products.Domain.ProductVariants;

namespace Pazario.Products.Domain.Products
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }
        public Name Name { get; set; }
        public Guid? ModelId { get; set; }
        public Model? Model { get; set; }
        public string Description { get; set; }
        public HashSet<ProductCategory> ProductCategories { get; set; }
        public HashSet<ProductVariant> Variants { get; set; }

    }
}
