using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Models
{
    public class Model : BaseEntity
    {
        public Model()
        {
            Products = new HashSet<Products.Product>();
        }
        public Name Name { get; set; }
        public Guid MarkaId { get; set; }
        public Markas.Marka Marka { get; init; }
        public HashSet<Products.Product> Products { get; init; }
    }
}
