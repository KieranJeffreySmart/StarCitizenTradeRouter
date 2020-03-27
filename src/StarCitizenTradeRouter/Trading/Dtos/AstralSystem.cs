using System.Collections.Generic;
using System.Linq;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class AstralSystem : NamedEntity
    {
        public virtual ICollection<Planet> Planets { get; set; }

        public IEnumerable<TradePoint> TradePoints => Planets.SelectMany(p => p.TradePoints).Union(Planets.SelectMany(p => p.Moons.SelectMany(m => m.TradePoints)));
    }
}
