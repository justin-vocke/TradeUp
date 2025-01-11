using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Behaviors;

namespace TradeUp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services) 
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));

            });

            return services;
        }
    }
}
