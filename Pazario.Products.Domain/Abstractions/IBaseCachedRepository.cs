using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Abstractions
{
    public interface IBaseCachedRepository<TEntity> : IBaseRepository<TEntity> 
                                where TEntity : BaseEntity
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
