using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<string>> GetAllDistinctTickersFromSubscriptions(CancellationToken cancellationToken = default)
        {
            var distinctTickers = await DbContext.Set<Subscription>()
                .GroupBy(x => x.TickerSymbol)
                .Select(group => group.First().TickerSymbol)
                .ToListAsync();

            return distinctTickers;
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken = default)
        {
            var distinctTickers = await DbContext.Set<Subscription>()
                .ToListAsync();

            return distinctTickers;
        }
    }
}
