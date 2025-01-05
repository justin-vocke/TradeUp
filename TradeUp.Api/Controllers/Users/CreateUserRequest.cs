namespace TradeUp.Api.Controllers.Users
{
    public sealed record CreateUserRequest(string FirstName, string LastName, string Email);
}
