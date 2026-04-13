using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pazario.Products.Infrastructure.Data;

namespace Pazario.Products.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyProductsMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ProductsDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
