using System;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class TradeOffer: ITradingEntity<int>
    {
        public int Id { get; set; }
        public Trader Trader { get; set; }
        public OfferType OfferType { get; set; }
        public int TradePointId { get; set; }
        public int CommodityId { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime DateTimeOfOffer { get; set; }
    }
}
