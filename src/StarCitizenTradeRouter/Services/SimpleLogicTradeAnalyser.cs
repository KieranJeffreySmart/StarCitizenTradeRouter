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
        private readonly ISimpleRepository<int, TradeOffer> offerRepo;
        private readonly ISimpleRepository<int, Planet> planetRepo;
        private string UEETraderName = "UEE";

        public SimpleLogicTradeAnalyser(ISimpleRepository<int, TradeOffer> offerRepo, ISimpleRepository<int, Planet> planetRepo)
        {
            this.offerRepo = offerRepo;
            this.planetRepo = planetRepo;
        }

        public async Task<IEnumerable<TradeOption>> GetBestNearPlanet(int max, int planetId, DateTime oldestTradeDate)
        {
            var planet = await planetRepo.Get(planetId);
            var localTradePointIds = planet.TradePoints.Union(planet.Moons.SelectMany(m => m.TradePoints)).Select(tp => tp.Id);

            // Get buys locally within date
            var buyTrades = await offerRepo.GetAll();
            var localBuyTrades = buyTrades.Where(t => t.Seller.Name == UEETraderName && t.DateTimeOfOffer >= oldestTradeDate && localTradePointIds.Contains(t.TradePoint.Id));
            var localCommodities = localBuyTrades.Select(t => t.Commodity.Id).Distinct().ToList();

            // Get sales for commodities and within date
            var sellTrades = await offerRepo.GetAll(t => t.Buyer.Name == UEETraderName && t.DateTimeOfOffer >= oldestTradeDate && localCommodities.Contains(t.Commodity.Id));

            // Get top 5 commodities
            var buyTradesPerCommodity = localBuyTrades.GroupBy(t => t.Commodity.Id).ToDictionary(t => t.Key, t => t.ToList());
            var salesForCommodities = sellTrades.Where(sale => buyTradesPerCommodity.ContainsKey(sale.Commodity.Id));
            Func<int, decimal> getAverageSalePriceByCommodity = (commodityId) => sellTrades.Where(sale => sale.Commodity.Id == commodityId).Average(sale => sale.PricePerUnit);
            var averageProffitPerCommodity = buyTradesPerCommodity
                .ToDictionary(
                tradeGroup => tradeGroup.Key,
                tradeGroup => getAverageSalePriceByCommodity(tradeGroup.Key) - tradeGroup.Value.Average(buy => buy.PricePerUnit));
            var topFiveCommodoties = averageProffitPerCommodity.OrderBy(kvp => kvp.Value).Take(max).Select(kvp => kvp.Key);

            // Filter top 5 commodities and group by trade point
            var buyTradesPerTradePointAndCommodity = localBuyTrades
                .Where(t => topFiveCommodoties.Contains(t.Commodity.Id))
                .GroupBy(t => new { t.TradePoint, t.Commodity }).ToDictionary(t => t.Key, t => t.ToList());

            return buyTradesPerTradePointAndCommodity.Select(g => new TradeOption
            {
                TradePointId = g.Key.TradePoint.Id,
                CommodityId = g.Key.Commodity.Id,
                AveragePricePerUnit = g.Value.Average(t => t.PricePerUnit),
                AverageProfitPerUnit = g.Value.Average(t => t.PricePerUnit) - getAverageSalePriceByCommodity(g.Key.Commodity.Id),
                RecentTrades = g.Value.OrderBy(t => t.DateTimeOfOffer).Take(5).ToList()
            }).OrderBy(to => to.AverageProfitPerUnit).Take(5);
        }
    }
}