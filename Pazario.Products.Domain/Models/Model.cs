using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Models
{
    public class Model : BaseEntity
    {
        private Model()
        {
            Products = new HashSet<Products.Product>();
        }
        public Name Name { get; private set; }
        public long MarkaId { get; private set; }
        public Markas.Marka Marka { get; private set; }
        public HashSet<Products.Product> Products { get; private set; }
    }
}
