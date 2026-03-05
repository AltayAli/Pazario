using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.ProductCategories
{
    public class ProductCategory : BaseEntity
    {
        public long ProductId { get; private set; }
        public Product Product { get; private set; }
        public long CategoryId { get; private set; }
        public Categories.Category Category { get; private set; }
    }
}
