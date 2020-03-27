using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Data.EntityFramework;
using StarCitizenTradeRouter.Trading.Dtos;

namespace StarCitizenTradeRouter.Data.Trading
{
    public class CommodityRepository : EntityFrameworkRepository<int, Commodity>
    {
        public CommodityRepository(TradingContext context, bool includeOnGet = false) : base(context, includeOnGet)
        {
        }
    }
}
