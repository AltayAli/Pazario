using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pazario.Products.Application.Behaviours;
using Pazario.Products.Application.Markas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddScoped<IMarkaExistenceChecker, MarkaExistenceChecker>();
        }
    }
}
