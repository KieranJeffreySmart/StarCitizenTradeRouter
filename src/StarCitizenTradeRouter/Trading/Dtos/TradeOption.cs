using StarCitizenTradeRouter.Trading.Dtos;
using System.Collections.Generic;

namespace StarCitizenTradeRouter.Dtos
{
    public class TradeOption
    {
        public int TradePointId { get; set; }
        public int CommodityId { get; set; }
        public decimal AveragePricePerUnit { get; set; }
        public List<TradeOffer> RecentTrades { get; set; }
        public decimal AverageProfitPerUnit { get; set; }
    }
}
