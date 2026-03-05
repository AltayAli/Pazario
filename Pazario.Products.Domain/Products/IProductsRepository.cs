using Pazario.Products.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Products
{
    public interface IProductsRepository : IBaseRepository<Product>
    {
    }
}
