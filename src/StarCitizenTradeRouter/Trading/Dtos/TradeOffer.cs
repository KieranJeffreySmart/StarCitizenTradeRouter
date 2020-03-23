using System;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class TradeOffer
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int TradePointId { get; set; }
        public int CommodityId { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime DateTimeOfOffer { get; set; }
    }
}
