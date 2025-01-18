using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Application.Commands.Subscriptions
{
    public class SubscriptionResponseBase
    {
        public decimal Threshold { get; init; }
        public string TickerSymbol { get; init; }
        public Guid Id { get; init; }
        public string Email { get; init; }
        public ThresholdPosition Position { get; init; }
    }
}
