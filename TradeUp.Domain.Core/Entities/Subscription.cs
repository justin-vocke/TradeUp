using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Abstractions;

namespace TradeUp.Domain.Core.Entities
{
    public class Subscription : Entity
    {

        private Subscription(Guid id, Guid userId, string email, 
            string tickerSymbol, decimal threshold, ThresholdPosition thresholdPosition) : base(id)
        {
            UserId = userId;
            Email = email;
            TickerSymbol = tickerSymbol;
            Threshold = threshold;
            Position = thresholdPosition;
        }

        private Subscription()
        {

        }

        public Guid UserId { get; set; }
       public string Email { get; set; }
        public string TickerSymbol { get; set; }
        public decimal Threshold { get; set; }
        public ThresholdPosition Position { get; set; }
        public bool UserNotified { get; set; }


        public static Subscription Create(Guid userId, string email, string tickerSymbol, 
            decimal threshold, ThresholdPosition thresholdPosition)
        {
            var subscription = new Subscription(Guid.NewGuid(), userId, email, tickerSymbol, threshold, thresholdPosition);

            return subscription;
        }

        public enum ThresholdPosition
        {
            Above,
            Below
        }

    }
}
