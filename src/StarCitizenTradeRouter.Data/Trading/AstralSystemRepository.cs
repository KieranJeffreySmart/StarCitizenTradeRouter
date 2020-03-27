using StarCitizenTradeRouter.Data.EntityFramework;
using StarCitizenTradeRouter.Services;
using StarCitizenTradeRouter.Trading.Dtos;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Data.Trading
{
    public class AstralSystemRepository : EntityFrameworkRepository<int, AstralSystem>, IAstralSystemRepository
    {
        TradingContext context;
        public AstralSystemRepository(TradingContext context, bool includeOnGetAll = false) : base(context, includeOnGetAll)
        {
            this.context = context;
        }

        public async Task<Planet> GetPlanet(int planetId)
        {
            var planet = await context.FindAsync<Planet>(planetId);
            await context.Entry(planet).Collection(p => p.Moons).LoadAsync();
            await context.Entry(planet).Collection(p => p.TradePoints).LoadAsync();

            foreach (var moon in planet.Moons)
            {
                await context.Entry(moon).Collection(m => m.TradePoints).LoadAsync();
            }

            return planet;
        }

        protected override async Task LoadEntity(AstralSystem entity)
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
        }
    }
}
