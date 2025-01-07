using System.Data;
using Bookify.Application.Abstractions.Messaging;
using Dapper;
using TradeUp.Application.Abstractions.Authentication;
using TradeUp.Application.Abstractions.Data;
using TradeUp.Application.Users.GetLoggedInUser;
using TradeUp.Domain.Core.Abstractions;

namespace Tradeup.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler
    : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContext _userContext;

    public GetLoggedInUserQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IUserContext userContext)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContext = userContext;
    }

    public async Task<Result<UserResponse>> Handle(
        GetLoggedInUserQuery request,
        CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                Id AS Id,
                FirstName AS FirstName,
                LastName AS LastName,
                email AS Email
            FROM users
            WHERE IdentityId = @IdentityId
            """;

        UserResponse user = await connection.QuerySingleAsync<UserResponse>(
            sql,
            new
            {
                _userContext.IdentityId
            });

        return user;
    }

    
}
