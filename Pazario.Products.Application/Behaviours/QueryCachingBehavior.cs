using MediatR;
using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Application.Caching;
using Pazario.Products.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Behaviours
{
    public class QueryCachingBehavior<TRequest, TResponse>
                                    (ICacheService _cacheService)
                                     : IPipelineBehavior<TRequest, TResponse>
                                     where TRequest : ICacheQuery<TResponse>
                                     where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cacheResult = await _cacheService.GetAsync<TResponse>(request.CacheKey);

            if (cacheResult is not null) 
            { 
                return cacheResult; 
            }
            else
            {
                var response = await next();
                if (response.IsSuccess)
                {
                    await _cacheService.SetAsync(request.CacheKey, response, request.Expiration);
                }
                return response;
            }
        }
    }
}
