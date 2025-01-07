using TradeUp.Domain.Core.Entities.Users;

namespace TradeUp.Infrastructure.Authorization
{
    public sealed class UserRolesResponse
    {
        public Guid Id { get; init; }
        public List<Role> Roles { get; init; } = [];
    }
}
