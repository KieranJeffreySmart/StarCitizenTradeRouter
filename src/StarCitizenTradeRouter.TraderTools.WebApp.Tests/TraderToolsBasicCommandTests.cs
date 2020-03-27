namespace StarCitizenTradeRouter.TraderTools.WebApp.Tests
{
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using StarCitizenTradeRouter.Data.EntityFramework;
    using StarCitizenTradeRouter.Data.Trading;
    using StarCitizenTradeRouter.Dtos;
    using StarCitizenTradeRouter.Presentation;
    using StarCitizenTradeRouter.Services;
    using StarCitizenTradeRouter.Trading.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xbehave;

    public class TraderToolsBasicCommandTests
    {
        [Scenario]
        public void SubmitTrader()
        {
            Trader trader = null;
            int id = 0;
            var dbName = "SubmitTrader";

            "Given I have a new trader".x(() => trader = TestTradeDataStatic.MargamanTrader);

            "When I submit the trader".x(async () =>
            {
                var traderRepository = new TraderRepository(CreateNewContext(dbName));
                await traderRepository.New(trader);
                id = trader.Id;
            });

            "Then the trader is displayed".x(async () =>
            {
                var traderRepository = new TraderRepository(CreateNewContext(dbName));
                AssertTrader(await traderRepository.Get(id), trader);
            });
        }

        [Scenario]
        public void SubmitSystem()
        {
            AstralSystem system = null;
            int id = 0;
            string dbName = "SubmitSystem";

            "Given I have a new system".x(() => system = TestSystemDataStatic.StantonSystem);

            "When I Submit the system".x(async () =>
            {
                var systemRepository = new AstralSystemRepository(CreateNewContext(dbName));
                await systemRepository.New(TestSystemDataStatic.StantonSystem);
                id = TestSystemDataStatic.StantonSystem.Id;
            });

            "Then the system is displayed".x(async () =>
            {
                var systemRepository = new AstralSystemRepository(CreateNewContext(dbName));
                AssertSystem(await systemRepository.Get(id), TestSystemDataStatic.StantonSystem);
            });
        }

        [Scenario]
        public void SubmitOffer()
        {
            Trader me = null;
            TradingPost post = null;
            Commodity commondity = null;
            NewTradeOffer offer = null;
            int id = 0;
            var dbName = "SubmitOffer";

            "Given I am a trader".x(async () =>
            {
                var traderRepository = new TraderRepository(CreateNewContext(dbName));
                id = await traderRepository.New(TestTradeDataStatic.MargamanTrader);
                me = await traderRepository.Get(id);
            });

            "And I have a trading post".x(async () =>
            {
                var systemRepository = new AstralSystemRepository(CreateNewContext(dbName));
                await systemRepository.New(TestSystemDataStatic.StantonSystem);
                var system = await systemRepository.Get(id);
                post = system.TradePoints.OfType<TradingPost>().First();
            });
        
            "And I have a new commodity".x(async () =>
            {
                var commodityRepo = new CommodityRepository(CreateNewContext(dbName));
                var id = await commodityRepo.New(TestTradeDataStatic.AgriciumCommondity);
                commondity = await commodityRepo.Get(id);
            });

            "And I have an offer".x(() => offer = new NewTradeOffer
            { 
                Id = 0, 
                TraderId = me.Id,
                TradePointId = post.Id, 
                CommodityId = commondity.Id, 
                PricePerUnit = 23.99M, 
                OfferType = (int)OfferType.Buy 
            });

            "When I submit the offer".x(async () =>
            {
                var traderRepository = new TraderRepository(CreateNewContext(dbName));
                var command = new NewTradeOfferCommand(traderRepository);
                id = await command.Execute(offer);
            });

            "Then the Offer is displayed".x(async () =>
            {
                var traderRepository = new TraderRepository(CreateNewContext(dbName), true);
                var trader = await traderRepository.Get(me.Id);
                AssertOffer(trader.TradeOffers.First(o => o.Id == id), offer);
            });
        }
        
        [Scenario]
        public void GetBestTradeFromLocation()
        {
            AstralSystem system = null;
            Planet location = null;
            List<TradeOption> results = null;
            var dbName = "GetBestTradeFromLocation";

            "Given I have a system with historical trades".x(async () => system = await GenerateSystem(CreateNewContext(dbName)));

            "And I have a search Location".x(() => location = system.Planets.First(p => p.Name == "ArcCorp"));
            "When I ask for the best trade".x(async () =>
            {
                var tradeAnalyser = new SimpleLogicTradeAnalyser(new TraderRepository(CreateNewContext(dbName), true), new AstralSystemRepository(CreateNewContext(dbName), true));
                results = (await tradeAnalyser.GetBestNearPlanet(5, location.Id, DateTime.Parse("15/03/2020"))).ToList();
            });

            "Then I am returned the top 5 trades".x(() => results.Count.Should().Be(expectedTrades));
        }

        private void AssertTrader(Trader actual, Trader expected)
        {
            actual.Id.Should().BeGreaterThan(0);
            actual.Name.Should().Be(expected.Name);
        }

        private void AssertSystem(AstralSystem actual, AstralSystem expected)
        {
            actual.Id.Should().BeGreaterThan(0);
            actual.Name.Should().Be(expected.Name);
        }

        private void AssertOffer(TradeOffer actual, NewTradeOffer expectedOffer)
        {
            actual.Should().NotBeNull();
            actual.Id.Should().BeGreaterThan(0);
            actual.TradePointId.Should().Be(expectedOffer.TradePointId);
            actual.CommodityId.Should().Be(expectedOffer.CommodityId);
            actual.PricePerUnit.Should().Be(expectedOffer.PricePerUnit);
            actual.Trader.Id.Should().Be(expectedOffer.TraderId);
            actual.OfferType.Should().Be(expectedOffer.OfferType);
        }

        private void AssertOffer(TradeOffer actual, TradeOffer expectedOffer)
        {
            actual.Should().NotBeNull();
            actual.Id.Should().BeGreaterThan(0);
            actual.TradePointId.Should().Be(expectedOffer.TradePointId);
            actual.CommodityId.Should().Be(expectedOffer.CommodityId);
            actual.PricePerUnit.Should().Be(expectedOffer.PricePerUnit);
            actual.Trader.Should().BeEquivalentTo(expectedOffer.Trader);
            actual.OfferType.Should().Be(expectedOffer.OfferType);
        }

        private static TradingContext CreateNewContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TradingContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            return new TradingContext(options);
        }

        private async Task<AstralSystem> GenerateSystem(DbContext context)
        {
            await context.AddRangeAsync(TestTradeDataStatic.AllCommodities);
            await context.SaveChangesAsync();

            var stanton = TestTradeDataStatic.GetStantonWithTradeData(TestTradeDataStatic.AllCommodities);
            context.Add(stanton);

            await context.SaveChangesAsync();

            return context.Find<AstralSystem>(stanton.Id);
        }
    }
}
