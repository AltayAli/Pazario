using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Pazario.Common.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyProductsMigrations<T_DbContext>(this IApplicationBuilder app) where T_DbContext : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T_DbContext>();
            dbContext.Database.Migrate();
        }
    }
}
