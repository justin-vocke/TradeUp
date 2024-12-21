using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Entities;

namespace TradeUp.Domain.Core.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<string>> GetAllDistinctTickersFromSubscriptions(CancellationToken cancellationToken = default);
        Task<IEnumerable<Subscription>> GetAllSubscriptions(CancellationToken cancellationToken = default);
        void Add(Subscription user);
    }
}
