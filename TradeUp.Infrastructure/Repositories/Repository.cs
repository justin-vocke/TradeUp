using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeUp.Infrastructure.Repositories
{
    internal abstract class Repository<T> where T : class
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Add(T entity)
        {
            DbContext.Add(entity);
        }

        public async Task<T?> GetByIdAsync(Guid id,
           CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            DbContext.Update(entity);
        }
    }
}
