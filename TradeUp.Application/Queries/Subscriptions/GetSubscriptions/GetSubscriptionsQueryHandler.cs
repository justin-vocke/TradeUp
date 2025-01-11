using Bookify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Application.Queries.Subscriptions.GetSubscriptions
{
    internal class GetSubscriptionsQueryHandler : IQueryHandler<GetSubscriptionsQuery, IReadOnlyList<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetSubscriptionsQueryHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result<IReadOnlyList<Subscription>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionRepository.GetAllSubscriptions(cancellationToken);

            return subscriptions.ToList();
        }
    }
}
