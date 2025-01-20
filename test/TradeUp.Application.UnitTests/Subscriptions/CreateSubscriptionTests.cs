using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.CommandHandlers.Subscriptions;
using TradeUp.Application.Commands.Subscriptions;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Interfaces.Repositories;
using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Application.UnitTests.Subscriptions
{
    public class CreateSubscriptionTests
    {
        private static readonly CreateSubscriptionCommand Command = new(
            Guid.NewGuid(),
            "test@test.com",
            150.50M,
            "IBM",
            ThresholdPosition.Below);

        private readonly CreateSubscriptionCommandHandler _handler;

        private readonly ISubscriptionRepository _subscriptionRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;

        public CreateSubscriptionTests()
        {
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _subscriptionRepositoryMock = Substitute.For<ISubscriptionRepository>();

            _handler = new CreateSubscriptionCommandHandler(
                _subscriptionRepositoryMock,
                _unitOfWorkMock);
        }
    }
}
