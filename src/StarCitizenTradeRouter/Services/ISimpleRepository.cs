using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Services
{
    public interface ISimpleRepository<TKey, TEntity>
    {
        Task New(TEntity entity);
        Task<TEntity> Get(TKey id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> whereClause);
    }
}