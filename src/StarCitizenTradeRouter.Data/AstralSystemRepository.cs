using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Data
{
    public class AstralSystemRepository : EntityFrameworkRepository<int, AstralSystem>
    {
        private readonly bool includeOnGetAll;

        public AstralSystemRepository(DbContext context, bool includeOnGetAll = false) : base(context)
        {
            this.includeOnGetAll = includeOnGetAll;
        }

        public override async Task<AstralSystem> Get(int id)
        {
            var entity = await base.Get(id);

            await LoadEntity(entity);

            return entity;
        }

        public override async Task<IEnumerable<AstralSystem>> GetAll()
        {
            var set = (await base.GetAll()).ToList();

            if (includeOnGetAll)
            {
                set.ForEach(async e => await LoadEntity(e));
            }

            return set;
        }

        public override async Task<IEnumerable<AstralSystem>> GetAll(Expression<Func<AstralSystem, bool>> whereClause)
        {
            var set = (await base.GetAll(whereClause)).ToList();

            if (includeOnGetAll)
            {
                set.ForEach(async e => await LoadEntity(e));
            }

            return set;
        }

        private async Task<AstralSystem> LoadEntity(AstralSystem entity)
        {
            await context.Entry(entity).Collection(s => s.Planets).LoadAsync();

            foreach (var planet in entity.Planets)
            {
                await context.Entry(planet).Collection(p => p.Moons).LoadAsync();
                await context.Entry(planet).Collection(p => p.TradePoints).LoadAsync();

                foreach (var moon in planet.Moons)
                {
                    await context.Entry(moon).Collection(m => m.TradePoints).LoadAsync();
                }
            }

            return entity;
        }
    }
}
