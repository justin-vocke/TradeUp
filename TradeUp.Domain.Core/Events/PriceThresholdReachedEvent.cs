using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Domain.Core.Events
{
    public class PriceThresholdReachedEvent : Event
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string TickerSymbol { get; set; }
        public decimal ThresholdPrice { get; set; }
        

        public PriceThresholdReachedEvent(Subscription subscription)
        {
            UserId = subscription.UserId;
            Email = subscription.Email;
            TickerSymbol = subscription.TickerSymbol;
            ThresholdPrice = subscription.Threshold;
        }
    }
}
