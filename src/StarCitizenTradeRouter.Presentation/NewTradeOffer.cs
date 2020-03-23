using System;
using System.Collections.Generic;
using System.Text;

namespace StarCitizenTradeRouter.Presentation
{
    public class NewTradeOffer
    {
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public int Id { get; set; }
        public int TradePointId { get; set; }
        public int CommodityId { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime DateTimeOfOffer { get; set; }
    }

    public class OfferRepositoryService
    {

    }
}
