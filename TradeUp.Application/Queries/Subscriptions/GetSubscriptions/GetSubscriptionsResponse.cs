using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Application.Queries.Subscriptions.GetSubscriptions
{
    public sealed class GetSubscriptionsResponse
    {
        public decimal Threshold { get; init; }
        public string TickerSymbol { get; init; }
        public Guid UserId { get; init; }
        public Guid Id { get; init; }
        public string Email { get; init; }
        public ThresholdPosition Position { get; init; }

        public bool UserNotified { get; init; }
    }
}
