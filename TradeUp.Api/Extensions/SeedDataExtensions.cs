using System.Data;
using Bogus;

using Dapper;
using TradeUp.Application.Abstractions.Data;
using static TradeUp.Domain.Core.Entities.Subscription;

namespace TradeUp.Api.Extensions;

internal static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();
        var userId = Guid.NewGuid();
        List<object> users = new();
        List<object> subscriptions = new();
       
        users.Add(new
        {
            Id = userId,
            Email = faker.Person.Email,
            IsActive = true,
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            IdentityId = Guid.NewGuid(),
        });
        

        for (int i = 0; i < 3; i++)
        {
            subscriptions.Add(new
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Email = faker.Person.Email,
                TickerSymbol = "IBM",
                Threshold = faker.Random.Double(),
                Position = ThresholdPosition.Below,
                UserNotified = 0
            });
        }
            const string sql = """
            INSERT INTO users
            (id, email, IsActive, FirstName, IdentityId, LastName)
            VALUES(@Id, @Email, @IsActive, @FirstName, @IdentityId ,@LastName)
            """;

        connection.Execute(sql, users);

        const string sqlSubscriptions = """
            INSERT INTO subscriptions
            (Threshold, TickerSymbol, UserId, Id, Email, Position, UserNotified)
            VALUES(@Threshold, @TickerSymbol, @UserId, @Id, @Email, @Position, @UserNotified);
            """;

        connection.Execute(sqlSubscriptions, subscriptions);
    }
}
