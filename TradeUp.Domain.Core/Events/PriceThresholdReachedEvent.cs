using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;
using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Domain.Core.Events
{
    public class PriceThresholdReachedEvent : Event
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string TickerSymbol { get; set; }
        public decimal ThresholdPrice { get; set; }
        public ThresholdPosition ThresholdPosition { get; set; }
        

        public PriceThresholdReachedEvent(Guid userId, string email, string tickerSymbol, 
            decimal thresholdPrice, ThresholdPosition thresholdPosition)
        {
            UserId = userId;
            Email = email;
            TickerSymbol = tickerSymbol;
            ThresholdPrice = thresholdPrice;
            ThresholdPosition = thresholdPosition;
        }
    }
}
