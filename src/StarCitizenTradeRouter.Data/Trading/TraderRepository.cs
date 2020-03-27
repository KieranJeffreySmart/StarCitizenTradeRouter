using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Data.EntityFramework;
using StarCitizenTradeRouter.Services;
using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Data.Trading
{
    public class TraderRepository : EntityFrameworkRepository<int, Trader>, ITraderRepository
    {
        TradingContext context;

        public TraderRepository(TradingContext context, bool includeOnGetAll = false) : base(context, includeOnGetAll)
        {
            this.context = context;
        }

        public async Task<int> AddTradeOffer(int traderId, TradeOffer tradeOffer)
        {
            var trader = await context.FindAsync<Trader>(traderId);
            await LoadEntity(trader);
            trader.TradeOffers.Add(tradeOffer);
            await context.SaveChangesAsync();
            return tradeOffer.Id;
        }

        public async Task<IEnumerable<TradeOffer>> GetBuyOffers(DateTime oldestTradeDate, IEnumerable<int> localTradePointIds)
        {
            return await context.Set<TradeOffer>().Where(
                t => t.OfferType == OfferType.Buy 
                && t.DateTimeOfOffer >= oldestTradeDate 
                && localTradePointIds.Contains(t.TradePointId))?.ToListAsync() 
                ?? Enumerable.Empty<TradeOffer>();
        }

        public async Task<IEnumerable<TradeOffer>> GetSellOffers(DateTime oldestTradeDate, List<int> localCommodities)
        {
            return await context.Set<TradeOffer>().Where(
                t => t.OfferType == OfferType.Sell 
                && t.DateTimeOfOffer >= oldestTradeDate 
                && localCommodities.Contains(t.CommodityId)).ToListAsync()
                ?? Enumerable.Empty<TradeOffer>();
        }

        protected override async Task LoadEntity(Trader entity)
        {
            await context.Entry(entity).Collection(to => to.TradeOffers).LoadAsync();
        }
    }
}
