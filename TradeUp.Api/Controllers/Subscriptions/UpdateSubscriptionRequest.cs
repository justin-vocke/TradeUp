using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Api.Controllers.Subscriptions
{
    public sealed record UpdateSubscriptionRequest(Guid Id, string Email, decimal Threshold, 
        string Ticker, ThresholdPosition Position);
}
