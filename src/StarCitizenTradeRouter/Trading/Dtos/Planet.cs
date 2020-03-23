using System.Collections.Generic;

namespace StarCitizenTradeRouter.Trading.Dtos
{
    public class Planet : AstralBody
    {
        public AstralSystem System { get; set; }
        public virtual ICollection<Moon> Moons { get; set; }
    }
}
