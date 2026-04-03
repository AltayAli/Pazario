using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.ProductCategories;
using Pazario.Products.Domain.Products.Events;
using Pazario.Products.Domain.ProductVariants;

namespace Pazario.Products.Domain.Products
{
    public class Product : BaseEntity
    {
        private Product()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }
        public Name Name { get; private set; }
        public Guid? ModelId { get; private set; }
        public Model? Model { get; private set; }
        public string Description { get; private set; }
        public HashSet<ProductCategory> ProductCategories { get; private set; }
        public HashSet<ProductVariant> Variants { get; private set; }

        public static Product Create(string name, string description, Guid? modelId = null)
        {
            var product = new Product
            {
                Name = (Name)name,
                Description = description,
                ModelId = modelId
            };
            product.AddDomainEvent(new ProductCreateEvent(product.Id));
            return product;
        }

        public Product Update(string name, string description, Guid? modelId = null)
        {
            Name = (Name)name;
            Description = description;
            ModelId = modelId;
            AddDomainEvent(new ProductUpdateEvent(Id));
            return this;
        }
    }
}
