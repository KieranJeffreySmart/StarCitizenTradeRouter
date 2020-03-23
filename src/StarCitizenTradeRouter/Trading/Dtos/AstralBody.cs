using System.Collections.Generic;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class AstralBody: Entity
    {
        public virtual ICollection<TradePoint> TradePoints { get; set; }
    }
}
