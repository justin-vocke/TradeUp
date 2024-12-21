using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradeup.Domain.Core.Bus;
using TradeUp.Domain.Core.Events;

namespace TradeUp.Application.EventHandlers
{
    public class PriceThresholdReachedEventHandler : IEventHandler<PriceThresholdReachedEvent>
    {
        //want to send email to user
        public Task Handle(PriceThresholdReachedEvent @event)
        {
            var message = $"Threshold of {@event.ThresholdPrice} was reached for stock {@event.TickerSymbol}";
            Console.WriteLine($"Threshold of {@event.ThresholdPrice} was reached for stock {@event.TickerSymbol}");
            var test = "stop here";
            return Task.CompletedTask;
        }
    }
}
