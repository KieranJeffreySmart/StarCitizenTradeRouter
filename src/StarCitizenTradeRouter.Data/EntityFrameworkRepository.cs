using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Data
{
    public class EntityFrameworkRepository<TKey, TEntity>: ISimpleRepository<TKey, TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        public EntityFrameworkRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task New(TEntity offer)
        {
            await context.AddAsync(offer);
            await context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Get(TKey id)
        {
            return await context.FindAsync<TEntity>(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> whereClause)
        {
            return await context.Set<TEntity>().Where(whereClause).ToListAsync();
        }
    }
}
