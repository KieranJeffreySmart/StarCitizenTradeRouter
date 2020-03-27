namespace StarCitizenTradeRouter.Data.EntityFramework
{
    using Microsoft.EntityFrameworkCore;
    using StarCitizenTradeRouter.Services;
    using StarCitizenTradeRouter.Trading.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class EntityFrameworkRepository<TKey, TEntity> : ISimpleRepository<TKey, TEntity> where TEntity : class, ITradingEntity<TKey>
    {
        private readonly DbContext context;
        private readonly bool includeOnGet;

        public EntityFrameworkRepository(DbContext context, bool includeOnGet = false)
        {
            this.context = context;
            this.includeOnGet = includeOnGet;
        }

        public async Task<TKey> New(TEntity entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<TEntity> Get(TKey id)
        {
            var entity = await context.FindAsync<TEntity>(id);

            if (includeOnGet)
            {
                await LoadEntity(entity);
            }

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await GetAll(e => true);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> whereClause)
        {
            var set = await context.Set<TEntity>().Where(whereClause).ToListAsync();

            if (includeOnGet)
            {
                set.ForEach(async e => await LoadEntity(e));
            }

            return set;
        }

        protected virtual async Task LoadEntity(TEntity entity)
        {
            // Nothing to see here...
            //TODO: Make this method a dependancy or make this class abstract
        }
    }
}
