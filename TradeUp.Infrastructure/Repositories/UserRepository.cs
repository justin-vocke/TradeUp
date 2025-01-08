using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities.Users;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Add(User entity)
        {
            foreach (var role in entity.Roles)
            {
                DbContext.Attach(role);
            }
            DbContext.Add(entity);
        }
    }
}
