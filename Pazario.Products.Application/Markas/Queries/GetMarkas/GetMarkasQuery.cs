using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Queries.GetMarkas
{
    public record GetMarkasQuery : ICacheQuery<List<GetMarkasResponse>>
    {
        public string Key { get; set; }

        public string CacheKey => CacheKeys.MarkasCacheKey;

        public TimeSpan? Expiration => TimeSpan.FromMinutes(30);
    }
}
