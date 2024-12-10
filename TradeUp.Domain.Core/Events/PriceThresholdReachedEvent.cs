using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Domain.Core.Events
{
    internal class PriceThresholdReachedEvent
    {
        public Guid UserId { get; set; }
        public string StockSymbol { get; set; }
        public decimal ThresholdPrice { get; set; }
        public decimal CurrentPrice { get; set; }

        public PriceThresholdReachedEvent(Guid userId, string stockSymbol, decimal thresholdPrice, decimal currentPrice)
        {
            UserId = userId;
            StockSymbol = stockSymbol;
            ThresholdPrice = thresholdPrice;
            CurrentPrice = currentPrice;
        }
    }
}
