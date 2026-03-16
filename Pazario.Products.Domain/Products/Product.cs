using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.Common;
using Pazario.Products.Domain.Models;
using Pazario.Products.Domain.ProductCategories;
using Pazario.Products.Domain.Products.Events;
using Pazario.Products.Domain.ProductVariants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Products
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }
        public Name Name { get; private set; }
        public Guid? ModelId { get; private set; }
        public Model? Model { get; private set; }
        public string Description { get; private set; }
        public HashSet<ProductCategory> ProductCategories { get; private set; }
        public HashSet<ProductVariant> Variants { get; private set; }

    }
}
