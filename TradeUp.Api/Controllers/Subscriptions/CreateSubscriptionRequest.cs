namespace TradeUp.Api.Controllers.Subscriptions
{
    public sealed record CreateSubscriptionRequest(Guid UserId, decimal Threshold, string Ticker);
}
