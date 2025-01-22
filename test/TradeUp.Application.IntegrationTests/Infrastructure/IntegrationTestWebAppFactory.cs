using DotNet.Testcontainers.Builders;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.Keycloak;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;
using Testcontainers.Redis;
using Tradeup.Domain.Core.Bus;
using Tradeup.Infrastructure.Authentication;
using Tradeup.Infrastructure.Bus;
using TradeUp.Api;
using TradeUp.Application.Abstractions.Data;
using TradeUp.Infrastructure;
using TradeUp.Infrastructure.Data;

namespace TradeUp.Application.IntegrationTests.Infrastructure
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<IApiAssemblyMarker>, IAsyncLifetime
    {
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>() // For user secrets in development
            .AddEnvironmentVariables() // For environment variables
            .Build();


        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPassword(_configuration["MsSqlContainer:Password"]) // Script to create your custom database
            .Build();

        private readonly RedisContainer _redisContainer = new RedisBuilder()
            .WithImage("redis:latest")
            .Build();

        private readonly KeycloakContainer _keycloakContainer = new KeycloakBuilder()
        .WithResourceMapping(
            new FileInfo(".files/bookify-realm-export.json"),
            new FileInfo("/opt/keycloak/data/import/realm.json"))
        .WithCommand("--import-realm")
        .Build();

        private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
             .WithImage("rabbitmq:management")
            .WithUsername("guest")
            .WithPassword("guest")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
             .Build();

        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(_dbContainer.GetConnectionString());
                });

                services.RemoveAll(typeof(ISqlConnectionFactory));

                services.AddSingleton<ISqlConnectionFactory>(_ =>
                new SqlConnectionFactory(_dbContainer.GetConnectionString()));

                services.Configure<RedisCacheOptions>(redisCacheOptions =>
                redisCacheOptions.Configuration = _redisContainer.GetConnectionString());

                services.RemoveAll(typeof(IEventBus));

                services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = _rabbitMqContainer.Hostname, // Dynamically set hostname
                        Port = _rabbitMqContainer.GetMappedPublicPort(5672), // Dynamically set port
                        UserName = "guest", // Default RabbitMQ credentials
                        Password = "guest"
                    };
                    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                    return new RabbitMQBus(sp.GetRequiredService<IMediator>(), 
                        scopeFactory, factory);
                });
                var keyCloakAddress = _keycloakContainer.GetBaseAddress();

                services.Configure<KeycloakOptions>(o =>
                {
                    o.AdminUrl = $"{keyCloakAddress}admin/realms/bookify/";
                    o.TokenUrl = $"{keyCloakAddress}realms/bookify/protocol/openid-connect/token";
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            await _redisContainer.StartAsync();
            await _keycloakContainer.StartAsync();
            await _rabbitMqContainer.StartAsync();
        }


        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
            await _redisContainer.StopAsync();
            await _keycloakContainer.StopAsync();
            await _rabbitMqContainer.StopAsync();
        }
    }
}
