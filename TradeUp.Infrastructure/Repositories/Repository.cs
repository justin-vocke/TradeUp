using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUp.Domain.Core.Abstractions;

namespace TradeUp.Infrastructure.Repositories
{
    internal abstract class Repository<T> where T : Entity
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
            return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(T entity)
        {
            var existingEntity = DbContext.Set<T>().Find(entity.Id);
            if (existingEntity != null)
            {
                DbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                DbContext.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
