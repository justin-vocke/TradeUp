using Bookify.Application.Abstractions.Messaging;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Abstractions.Data;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Application.Queries.Subscriptions.GetSubscriptions
{
    internal class GetSubscriptionsQueryHandler : IQueryHandler<GetSubscriptionsQuery, IReadOnlyList<GetSubscriptionsResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetSubscriptionsQueryHandler(ISubscriptionRepository subscriptionRepository, ISqlConnectionFactory sqlConnectionFactory)
        {
            _subscriptionRepository = subscriptionRepository;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<GetSubscriptionsResponse>>> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

            const string sql = """
            SELECT
                Id,
                UserId,
                Email,
                Position,
                UserNotified,
                TickerSymbol,
                Threshold
            FROM subscriptions
            """;

            var subscriptions = await connection.QueryAsync<GetSubscriptionsResponse>(sql);  

            //var subscriptions = await _subscriptionRepository.GetAllSubscriptions(cancellationToken);

            return subscriptions.ToList();
        }
    }
}
