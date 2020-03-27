using StarCitizenTradeRouter.Dtos;
using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Services
{
    public class SimpleLogicTradeAnalyser: ITradeAnaliser
    {
        private readonly ITraderRepository traderRepo;
        private readonly IAstralSystemRepository systemRepo;
        private string UEETraderName = "UEE";

        public SimpleLogicTradeAnalyser(ITraderRepository traderRepo, IAstralSystemRepository systemRepo)
        {
            this.traderRepo = traderRepo;
            this.systemRepo = systemRepo;
        }

        public async Task<IEnumerable<TradeOption>> GetBestNearPlanet(int max, int planetId, DateTime oldestTradeDate)
        {
            var planet = await systemRepo.GetPlanet(planetId);
            var localTradePointIds = planet.TradePoints.Union(planet.Moons.SelectMany(m => m.TradePoints)).Select(tp => tp.Id);

            // Get buys locally within date
            var buyTrades = await traderRepo.GetBuyOffers(oldestTradeDate, localTradePointIds);
            var localCommodities = buyTrades.Select(t => t.CommodityId).Distinct().ToList();

            // Get sales for commodities and within date
            var sellTrades = await traderRepo.GetSellOffers(oldestTradeDate, localCommodities);
            
            // Get top 5 commodities
            var buyTradesPerCommodity = buyTrades.GroupBy(t => t.CommodityId).ToDictionary(t => t.Key, t => t.ToList());
            var salesForCommodities = sellTrades.Where(sale => buyTradesPerCommodity.ContainsKey(sale.CommodityId));
            Func<int, decimal> getAverageSalePriceByCommodity = (commodityId) => sellTrades.Where(sale => sale.CommodityId == commodityId).Average(sale => sale.PricePerUnit);
            var averageProffitPerCommodity = buyTradesPerCommodity
                .ToDictionary(
                tradeGroup => tradeGroup.Key,
                tradeGroup => getAverageSalePriceByCommodity(tradeGroup.Key) - tradeGroup.Value.Average(buy => buy.PricePerUnit));
            var topFiveCommodoties = averageProffitPerCommodity.OrderBy(kvp => kvp.Value).Take(max).Select(kvp => kvp.Key);

            // Filter top 5 commodities and group by trade point
            var buyTradesPerTradePointAndCommodity = buyTrades
                .Where(t => topFiveCommodoties.Contains(t.CommodityId))
                .GroupBy(t => new { t.TradePointId, t.CommodityId }).ToDictionary(t => t.Key, t => t.ToList());

            return buyTradesPerTradePointAndCommodity.Select(g => new TradeOption
            {
                TradePointId = g.Key.TradePointId,
                CommodityId = g.Key.CommodityId,
                AveragePricePerUnit = g.Value.Average(t => t.PricePerUnit),
                AverageProfitPerUnit = g.Value.Average(t => t.PricePerUnit) - getAverageSalePriceByCommodity(g.Key.CommodityId),
                RecentTrades = g.Value.OrderBy(t => t.DateTimeOfOffer).Take(5).ToList()
            }).OrderBy(to => to.AverageProfitPerUnit).Take(5);
        }
    }
}