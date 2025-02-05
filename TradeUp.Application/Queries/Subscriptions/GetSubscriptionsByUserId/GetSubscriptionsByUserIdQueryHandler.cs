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

namespace TradeUp.Application.Queries.Subscriptions.GetSubscriptionsByUserId
{
    internal class GetSubscriptionsByUserIdQueryHandler : IQueryHandler<GetSubscriptionsByUserIdQuery, IReadOnlyList<GetSubscriptionsByUserIdResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetSubscriptionsByUserIdQueryHandler( ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<GetSubscriptionsByUserIdResponse>>> Handle(GetSubscriptionsByUserIdQuery request, CancellationToken cancellationToken)
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
            WHERE userid = @userid
            """;

            var subscriptions = await connection.QueryAsync<GetSubscriptionsByUserIdResponse>(sql,
                new
                {
                    request.UserId
                });

            return subscriptions.ToList();
        }
    }
}
