using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Abstractions.Messaging
{
    public interface ICacheQuery<TResponse> : IQuery<TResponse>, ICacheQuery;
    public interface ICacheQuery
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
