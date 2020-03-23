using StarCitizenTradeRouter.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Services
{
    public interface ITradeAnaliser
    {
        Task<IEnumerable<TradeOption>> GetBestNearPlanet(int max, int planetId, DateTime oldestTradeDate);
    }
}