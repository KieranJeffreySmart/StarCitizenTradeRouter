using System.Collections.Generic;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class AstralBody: NamedEntity
    {
        public virtual ICollection<TradePoint> TradePoints { get; set; }
    }
}
