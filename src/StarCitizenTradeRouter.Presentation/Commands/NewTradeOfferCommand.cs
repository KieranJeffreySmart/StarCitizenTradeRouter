using StarCitizenTradeRouter.Services;
using StarCitizenTradeRouter.Trading.Dtos;
using System.Threading.Tasks;

namespace StarCitizenTradeRouter.Presentation
{
    public class NewTradeOfferCommand
    {
        private readonly ITraderRepository traderRepo;

        public NewTradeOfferCommand(ITraderRepository traderRepo)
        {
            this.traderRepo = traderRepo;
        }

        public async Task<int> Execute(NewTradeOffer data)
        {
            return await traderRepo.AddTradeOffer(data.TraderId, new TradeOffer
            {
                TradePointId = data.TradePointId,
                CommodityId = data.CommodityId,
                DateTimeOfOffer = data.DateTimeOfOffer,
                PricePerUnit = data.PricePerUnit,
                OfferType = (OfferType)data.OfferType
            });
        }
    }
}
