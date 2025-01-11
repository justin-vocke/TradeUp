using Bookify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Caching;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Queries.Subscriptions.GetSubscriptions
{
    public sealed record GetSubscriptionsQuery : ICachedQuery<IReadOnlyList<Subscription>>
    {
        public string CacheKey => $"subscriptions:all";

        public TimeSpan? Expiration => null;
    }
}
