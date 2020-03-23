using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Data
{
    public class PlanetRepository : EntityFrameworkRepository<int, Planet>
    {
        private readonly bool includeOnGetAll;
        public PlanetRepository(DbContext context, bool includeOnGetAll = false) : base(context)
        {
            this.includeOnGetAll = includeOnGetAll;
        }

        public override async Task<Planet> Get(int id)
        {
            var entity = await base.Get(id);

            await LoadEntity(entity);

            return entity;
        }

        public override async Task<IEnumerable<Planet>> GetAll()
        {
            var set = (await base.GetAll()).ToList();

            if (includeOnGetAll)
            {
                set.ForEach(async e => await LoadEntity(e));
            }

            return set;
        }

        public override async Task<IEnumerable<Planet>> GetAll(Expression<Func<Planet, bool>> whereClause)
        {
            var set = (await base.GetAll(whereClause)).ToList();

            if (includeOnGetAll)
            {
                set.ForEach(async e => await LoadEntity(e));
            }

            return set;
        }

        private async Task<Planet> LoadEntity(Planet entity)
        {
            await context.Entry(entity).Collection(p => p.Moons).LoadAsync();
            await context.Entry(entity).Collection(s => s.TradePoints).LoadAsync();

            foreach (var moon in entity.Moons)
            {
                await context.Entry(moon).Collection(m => m.TradePoints).LoadAsync();
            }

            return entity;
        }
    }
}
