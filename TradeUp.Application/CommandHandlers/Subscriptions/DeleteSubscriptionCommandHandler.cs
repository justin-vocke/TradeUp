using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Application.Commands.Subscriptions;
using TradeUp.Application.Exceptions;
using TradeUp.Application.Abstractions;
using TradeUp.Domain.Core.Abstractions;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Interfaces.Repositories;
using TradeUp.Application.Abstractions.Messaging;
using System.Data;
using TradeUp.Application.Abstractions.Data;
using TradeUp.Application.Queries.Subscriptions.GetSubscriptionsByUserId;
using Dapper;

namespace TradeUp.Application.CommandHandlers.Subscriptions
{
    internal class DeleteSubscriptionCommandHandler : ICommandHandler<DeleteSubscriptionCommand, bool>
    {
        
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public DeleteSubscriptionCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }


        public async Task<Result<bool>> Handle(DeleteSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

                const string sql = """
                    DELETE
                    FROM subscriptions
                    WHERE userid = @UserId and id = @SubId
                    """;

                var recordsDeletedCount = await connection.ExecuteAsync(sql,
                    new
                    {
                        UserId = request.UserId,
                        SubId = request.Id
                    });

                if(recordsDeletedCount == 1)
                {
                    return Result.Success(true);
                }
                else
                {
                    return Result.Failure<bool>(new Error("Operation Error", "Error processing your request"));
                }
                
                
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<bool>(new Error("Subscription.Overlap", "Error processing new stock subsription"));
            }
        }
    }
}
