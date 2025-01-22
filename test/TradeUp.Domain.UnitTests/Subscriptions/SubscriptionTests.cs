using FluentAssertions;
using FluentAssertions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Domain.UnitTests.Subscriptions
{
    public class SubscriptionTests
    {
        [Fact]
        public void Create_Should_AddSubscriptionProperties()
        {
            //Act
            var subscription = Subscription.Create(SubscriptionData.UserId, SubscriptionData.Email,
                SubscriptionData.Ticker, SubscriptionData.Threshold, SubscriptionData.ThresholdPosition);

            //Assert
            subscription.UserId.Should().Be(SubscriptionData.UserId);
            subscription.Email.Should().Be(SubscriptionData.Email);
            subscription.TickerSymbol.Should().Be(SubscriptionData.Ticker);
            subscription.Threshold.Should().Be(SubscriptionData.Threshold);
            subscription.Position.Should().Be(SubscriptionData.ThresholdPosition);

        }
    }
}
