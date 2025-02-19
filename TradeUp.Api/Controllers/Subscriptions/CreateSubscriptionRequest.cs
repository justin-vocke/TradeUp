﻿using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Api.Controllers.Subscriptions
{
    public sealed record CreateSubscriptionRequest(string Email, decimal Threshold, 
        string Ticker, ThresholdPosition Position);
}
