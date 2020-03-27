using StarCitizenTradeRouter.Trading.Dtos;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Services
{
    public interface IAstralSystemRepository : ISimpleRepository<int, AstralSystem>
    {
        Task<Planet> GetPlanet(int planetId);
    }
}