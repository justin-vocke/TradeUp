using Bookify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Caching;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Application.Queries.Subscriptions.GetSubscriptionsByUserId
{
    public sealed record GetSubscriptionsByUserIdQuery(string UserId) : ICachedQuery<IReadOnlyList<GetSubscriptionsByUserIdResponse>>
    {
        public string CacheKey => $"subscriptions:all{UserId}";

        public TimeSpan? Expiration => null;
    }
}
