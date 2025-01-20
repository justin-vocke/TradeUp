using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Domain.UnitTests.Subscriptions
{
    internal static class SubscriptionData
    {
        public static readonly Guid UserId = Guid.NewGuid();
        public static readonly string Email = "test@test.com";
        public static readonly string Ticker = "ABC";
        public static readonly decimal Threshold = 100.20M;
        public static readonly ThresholdPosition ThresholdPosition = ThresholdPosition.Above;
    }
}
