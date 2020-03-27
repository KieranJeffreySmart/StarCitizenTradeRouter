using System.Collections.Generic;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class Trader: NamedEntity
    {
        public virtual ICollection<TradeOffer> TradeOffers { get; set; }
    }
}
