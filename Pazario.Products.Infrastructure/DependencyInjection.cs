using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pazario.Common.Infrastructure.Extensions;
using Pazario.Products.Application.Markas;
using Pazario.Products.Application.Models;
using Pazario.Products.Domain.Abstractions;
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
            services.AddScoped<IMarkaExistenceChecker, MarkaExistenceChecker>();
            services.AddScoped<IModelExistenceChecker, ModelExistenceChecker>();

            AddInfrastructure(services, configuration);

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(CategoryMappingProfile).Assembly));
        }
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            
            services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ProductsDbContext>());

            services.AddRepositories<ProductsDbContext>();
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


    }
}