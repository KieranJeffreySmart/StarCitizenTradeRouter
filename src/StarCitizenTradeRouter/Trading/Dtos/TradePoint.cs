using System.Collections.Generic;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class TradePoint : NamedEntity
    {
        public AstralBody Parent { get; set; }

        public virtual ICollection<TradeOffer> TradeOffers { get; set; }
    }
}
