using System;

namespace StarCitizenTradeRouter.Presentation
{
    public class NewTradeOffer
    {
        public int TraderId { get; set; }
        public int OfferType { get; set; }
        public int Id { get; set; }
        public int TradePointId { get; set; }
        public int CommodityId { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime DateTimeOfOffer { get; set; }
    }
}
