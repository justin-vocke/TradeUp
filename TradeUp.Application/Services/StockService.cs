using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradeup.Domain.Core.Bus;
using TradeUp.Application.Commands;
using TradeUp.Application.Interfaces;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IEventBus _eventBus;

        public StockService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void SendStockThresholdNotification(Subscription subscription)
        {
            var createPriceThresholdNotificationCommand = new CreatePriceThresholdNotificationCommand
            (
                subscription.UserId,
                subscription.Email,
                subscription.TickerSymbol,
                subscription.Threshold
            );

            _eventBus.SendCommand( createPriceThresholdNotificationCommand );
        }
    }
}
