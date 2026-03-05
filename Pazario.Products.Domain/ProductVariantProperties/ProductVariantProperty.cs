using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryProperties;
using Pazario.Products.Domain.ProductVariants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.ProductVariantProperties
{
    public class ProductVariantProperty : BaseEntity
    {
        private ProductVariantProperty()
        {
        }
        public long ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; }
        public long CategoryPropertyId { get; private set; }
        public CategoryProperty CategoryProperty { get; private set; }
        public string Value { get; private set; }
    }
}
