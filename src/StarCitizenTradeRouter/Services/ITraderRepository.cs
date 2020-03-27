using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Services
{
    public interface ITraderRepository : ISimpleRepository<int, Trader>
    {
        Task<IEnumerable<TradeOffer>> GetBuyOffers(DateTime oldestTradeDate, IEnumerable<int> localTradePointIds);
        Task<IEnumerable<TradeOffer>> GetSellOffers(DateTime oldestTradeDate, List<int> localCommodities);
        Task<int> AddTradeOffer(int traderId, TradeOffer tradeOffer);
    }
}