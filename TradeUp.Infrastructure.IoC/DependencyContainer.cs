using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tradeup.Domain.Core.Bus;
using Tradeup.Infrastructure.Bus;
using TradeUp.Application.CommandHandlers;
using TradeUp.Application.Commands;
using TradeUp.Application.EventHandlers;
using TradeUp.Application.Interfaces;
using TradeUp.Application.Services;
using TradeUp.Domain.Core.Events;

namespace TradeUp.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetRequiredService<IMediator>(), scopeFactory);
            });

            

            //Domain Stock services
            services.AddTransient<IRequestHandler<CreatePriceThresholdNotificationCommand, bool>, PriceThresholdNotificationCommandHandler>();
            services.AddTransient<IStockService, StockService>();
            //Domain events
            services.AddTransient<IEventHandler<PriceThresholdReachedEvent>, PriceThresholdReachedEventHandler>();
            //Subscriptions
            services.AddTransient<PriceThresholdReachedEventHandler>();
        }

        
    }
}
