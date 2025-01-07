using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TradeUp.Infrastructure.Authentication;
using TradeUp.Application.Abstractions;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Interfaces.Repositories;
using TradeUp.Infrastructure.Authorization;
using TradeUp.Infrastructure.Repositories;
using Tradeup.Infrastructure.Authentication;
using AuthenticationService = TradeUp.Infrastructure.Authentication.AuthenticationService;
using AuthenticationOptions = TradeUp.Infrastructure.Authentication.AuthenticationOptions;
using IAuthenticationService = TradeUp.Application.Abstractions.IAuthenticationService;

namespace TradeUp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            AddPersistence(services, configuration);
            AddAuthentication(services, configuration);
            AddAuthorization(services);

            return services;
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer();

            services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var keyCloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
                httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            {
                var keyCloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
                httpClient.BaseAddress = new Uri(keyCloakOptions.TokenUrl);
            });
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

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();

            services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
        }
    }
}
