using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tradeup.Domain.Core.Bus;
using Tradeup.Infrastructure.Bus;
using TradeUp.Application.CommandHandlers;
using TradeUp.Application.Commands;
using TradeUp.Application.EventHandlers;
using TradeUp.Application.Abstractions;
using TradeUp.Application.Services;
using TradeUp.Domain.Core.Events;
using TradeUp.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace TradeUp.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {

            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = configuration["RabbitMQ:HostName"], // From appsettings.json or environment
                    Port = int.Parse(configuration["RabbitMQ:Port"]),
                    UserName = configuration["RabbitMQ:UserName"],
                    Password = configuration["RabbitMQ:Password"]
                };
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetRequiredService<IMediator>(), scopeFactory, factory);
            });

            

            //Domain Stock services
            services.AddTransient<IStockService, StockService>();
            //Domain events
            services.AddTransient<IEventHandler<PriceThresholdReachedEvent>, PriceThresholdReachedEventHandler>();
            //Subscriptions
            services.AddTransient<PriceThresholdReachedEventHandler>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }

        
    }
}
