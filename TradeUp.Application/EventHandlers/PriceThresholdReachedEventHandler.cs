using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradeup.Domain.Core.Bus;
using TradeUp.Application.Abstractions.Notifications.Email;
using TradeUp.Domain.Core.Events;

namespace TradeUp.Application.EventHandlers
{
    public class PriceThresholdReachedEventHandler : IEventHandler<PriceThresholdReachedEvent>
    {
        private readonly IEmailService _emailService;

        public PriceThresholdReachedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        //want to send email to user
        public Task Handle(PriceThresholdReachedEvent @event)
        {
            var message = $"Threshold of {@event.ThresholdPrice} was reached for stock {@event.TickerSymbol}";
            _emailService.SendEmailAsync(@event.Email, "Price Reached For A Stock You Follow", message );
            
            return Task.CompletedTask;
        }
    }
}
