using System.Collections.Generic;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class Trader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TradeOffer> TradeOffers { get; set; }
    }
}
