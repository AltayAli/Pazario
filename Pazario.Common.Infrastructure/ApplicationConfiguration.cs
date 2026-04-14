using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pazario.Common.Application.Abstractions;
using Pazario.Common.Application.Behaviours;
using Pazario.Common.Application.Caching;
using Pazario.Common.Infrastructure.Caching;
using Pazario.Common.Infrastructure.Clock;
using System.Reflection;

namespace Pazario.Common.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services, 
                                                IConfiguration configuration,
                                                Assembly[] assemblies)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
            });

            services.AddValidatorsFromAssemblies(assemblies);

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICacheService, CacheService>();

            AddCahce(services, configuration);

            services.AddHttpContextAccessor();
        }
        private static IServiceCollection AddCahce(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("CacheConnection");

            services.AddStackExchangeRedisCache(options => { options.Configuration = connectionString; });

            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
