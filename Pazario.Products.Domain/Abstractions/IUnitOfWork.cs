using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
