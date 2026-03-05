using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.ProductVariants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.ProductVariantImages
{
    public class ProductVariantImage : BaseEntity
    {
        private ProductVariantImage()
        {
        }

        public long ProductVariantId { get; private set; }
        public ProductVariant ProductVariant { get; private set; }
        public ImageUrl ImageUrl { get; private set; }
        public bool IsMain { get; private set; }
    }
}
