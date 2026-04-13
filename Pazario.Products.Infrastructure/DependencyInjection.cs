using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pazario.Products.Application;
using Pazario.Products.Application.Abstractions;
using Pazario.Products.Application.Behaviours;
using Pazario.Products.Application.Caching;
using Pazario.Products.Application.Markas;
using Pazario.Products.Application.Models;
using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Infrastructure.Caching;
using Pazario.Products.Infrastructure.Clock;
using Pazario.Products.Infrastructure.Data;
using Pazario.Products.Infrastructure.Helpers;
using Pazario.Products.Presentation.Categories;
using Pazario.Products.Presentation.Categories.Mapping;
using Pazario.Products.Presentation.CategoryProperties;
using Pazario.Products.Presentation.Markas;
using Pazario.Products.Presentation.Models;
using Pazario.Products.Presentation.Products;

namespace Pazario.Products.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddProductModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

            services.AddScoped<IMarkaExistenceChecker, MarkaExistenceChecker>();
            services.AddScoped<IModelExistenceChecker, ModelExistenceChecker>();
            AddInfrastructure(services, configuration);
            AddCahce(services, configuration);
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(CategoryMappingProfile).Assembly));
        }
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddHttpContextAccessor();
            
            services.AddDbContext<ProductsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductsDbContext>());

            AddRepositories(services);

            services.AddScoped<IMarkaExistenceChecker, MarkaExistenceChecker>();
        }

        private static IServiceCollection AddCahce(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("CacheConnection");

            services.AddStackExchangeRedisCache(options => { options.Configuration = connectionString; });

            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }

        public static IEndpointRouteBuilder MapProductModuleEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapCategoryEndpoints();
            app.MapCategoryPropertyEndpoints();
            app.MapMarkaEndpoints();
            app.MapModelEndpoints();
            app.MapProductEndpoints();
            app.MapProductVariantEndpoints();

            return app;
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ProductsDbContext>()
                .AddClasses(classes => classes
                    .Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}