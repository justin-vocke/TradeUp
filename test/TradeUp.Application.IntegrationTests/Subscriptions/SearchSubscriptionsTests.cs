using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.IntegrationTests.Infrastructure;
using TradeUp.Application.Queries.Subscriptions.GetSubscriptions;

namespace TradeUp.Application.IntegrationTests.Subscriptions
{
    public class SearchSubscriptionsTests : BaseIntegrationTest
    {
        public SearchSubscriptionsTests(IntegrationTestWebAppFactory factory)
            :base(factory)
        {
            
        }

        [Fact]
        public async Task GetSubscriptions_Should_ReturnSubscriptionList()
        {
            //Arrange
            var query = new GetSubscriptionsQuery();

            //Act
            var result = await Sender.Send(query);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
        }
    }
}
