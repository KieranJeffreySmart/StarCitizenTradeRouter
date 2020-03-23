using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Data;
using StarCitizenTradeRouter.Dtos;
using StarCitizenTradeRouter.Services;
using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xbehave;

namespace StarCitizenTradeRouter.TraderTools.WebApp.Tests
{
    public class TraderToolsBasicCommandTests
    {
        [Scenario]
        public void SubmitTrader()
        {
            Trader trader = null;
            int id = 0;

            "Given I have a new Trader".x(() => trader = TestTradeDataStatic.MargamanTrader);

            "When I Submit the Trader".x(async () =>
            {
                var traderRepository = CreateRepository<int, Trader>("SubmitTrader");
                await traderRepository.New(trader);
                id = trader.Id;
            });

            "Then the Trader is displayed".x(async () =>
            {
                var traderRepository = CreateRepository<int, Trader>("SubmitTrader");
                AssertTrader(await traderRepository.Get(id), trader);
            });
        }

        [Scenario]
        public void SubmitSystem()
        {
            AstralSystem system = null;
            int id = 0;

            "Given I have a new Trader".x(() => system = TestSystemDataStatic.StantonSystem);

            "When I Submit the Trader".x(async () =>
            {
                var systemRepository = CreateRepository<int, AstralSystem>("SubmitSystem");
                await systemRepository.New(TestSystemDataStatic.StantonSystem);
                id = TestSystemDataStatic.StantonSystem.Id;
            });

            "Then the Trader is displayed".x(async () =>
            {
                var systemRepository = new AstralSystemRepository(CreateNewContext("SubmitSystem"), true);
                AssertSystem(await systemRepository.Get(id), TestSystemDataStatic.StantonSystem);
            });
        }

        [Scenario]
        public void SubmitOffer()
        {
            Trader buyer = null;
            Trader seller = null;
            TradingPost post = null;
            Commodity commondity = null;
            TradeOffer offer = null;
            int id = 0;
            var dbName = "SubmitOffer";

            "Given I have a buyer".x(async () =>
            {
                var traderRepo = CreateRepository<int, Trader>(dbName);
                await traderRepo.New(TestTradeDataStatic.MargamanTrader);
                buyer = traderRepo
            });

            "And I have a seller".x(() => seller = TestTradeDataStatic.UEETrader);

            "And I have a trading post".x(() => post = TestSystemDataStatic.KudreOrePost);

            "And I have a new type of goods".x(() => commondity = TestTradeDataStatic.AgriciumCommondity);

            "And I have an offer".x(() => offer = new TradeOffer { Id = 0, TradePointId = post, Commodity = commondity, PricePerUnit = 23.99M, Buyer = buyer, Seller = seller });

            "When I submit the offer".x(async () =>
            {
                var offerRepository = CreateRepository<int, TradeOffer>();
                await offerRepository.New(offer);
                id = offer.Id;
            });

            "Then the Offer is displayed".x(async () =>
            {
                var offerRepository = CreateRepository<int, TradeOffer>("SubmitOffer");
                AssertOffer(await offerRepository.Get(id), offer);
            });
            
        }


        [Scenario]
        public void GetBestTradeFromLocation()
        {
            AstralSystem system = null;
            Planet location = null;
            List<TradeOption> results = null;
            var dbName = "GetBestTradeFromLocation";

            List<TradeOption> expectedTrades = new List<TradeOption>
            {

            };

            "Given I have a system with historical trades".x(async () => system = await GenerateSystem(CreateNewContext(dbName)));
            "And the system has been submitted".x(async () =>
            {
                var systemRepo = new AstralSystemRepository(CreateNewContext(dbName));
                await systemRepo.New(system);
            });
            "And I have a search Location".x(() => location = system.Planets.First(p => p.Name == "ArcCorp"));
            "When I ask for the best trade".x(async () =>
            {
                var tradeAnalyser = new SimpleLogicTradeAnalyser(new TradeOfferRepository(CreateNewContext(dbName), true), new PlanetRepository(CreateNewContext(dbName), true));
                results = (await tradeAnalyser.GetBestNearPlanet(5, location.Id, DateTime.Parse("15/03/2020"))).ToList();
            });
            "Then I am returned the top 5 trades".x(() => results.Should().BeEquivalentTo(expectedTrades));
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

        private void AssertOffer(TradeOffer actual, TradeOffer expectedOffer)
        {
            actual.Should().NotBeNull();
            actual.Id.Should().BeGreaterThan(0);
            actual.TradePoint.Should().BeEquivalentTo(expectedOffer.TradePoint);
            actual.PricePerUnit.Should().Be(expectedOffer.PricePerUnit);
            actual.Buyer.Should().BeEquivalentTo(expectedOffer.Buyer);
            actual.Seller.Should().BeEquivalentTo(expectedOffer.Seller);
        }

        private static ISimpleRepository<TKey, TEntity> CreateRepository<TKey, TEntity>(string dbName) where TEntity : class
        {
            return new EntityFrameworkRepository<TKey, TEntity>(CreateNewContext(dbName));
        }

        private static DbContext CreateNewContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TradingContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            var ctx = new TradingContext(options);
            return ctx;
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
