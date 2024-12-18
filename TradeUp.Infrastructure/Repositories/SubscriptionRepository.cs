using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;
using TradeUp.Domain.Core.Interfaces.Repositories;

namespace TradeUp.Infrastructure.Repositories
{
    internal sealed class SubscriptionRepository: Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
