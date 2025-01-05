using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradeup.Infrastructure.Authentication;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Interfaces.Repositories;
using TradeUp.Infrastructure.Authentication;
using TradeUp.Infrastructure.Repositories;

namespace TradeUp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            AddPersistence(services, configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, HttpClient) =>
            {
                var keyCloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
                HttpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();
            return services;
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                            configuration.GetConnectionString("DefaultConnection") ??
                            throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        }
    }
}
