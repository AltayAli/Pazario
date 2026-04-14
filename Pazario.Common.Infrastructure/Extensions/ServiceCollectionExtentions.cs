using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Pazario.Common.Infrastructure.Extensions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddRepositories<T_DbContext>(this IServiceCollection services) where T_DbContext : DbContext
        {
            services.Scan(scan => scan
                .FromAssemblyOf<T_DbContext>()
                .AddClasses(classes => classes
                    .Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
