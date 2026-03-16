using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Markas
{
    public class Marka : BaseEntity
    {
        public Marka()
        {
            Models = new HashSet<Models.Model>();
        }
        public Name Name { get; set; }
        public HashSet<Models.Model> Models { get; private set; }
    }
}
