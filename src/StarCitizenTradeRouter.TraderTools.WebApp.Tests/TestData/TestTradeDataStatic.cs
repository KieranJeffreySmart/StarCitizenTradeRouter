using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StarCitizenTradeRouter.TraderTools.WebApp.Tests
{
    public static class TestTradeDataStatic
    {
        public static Trader UEETrader = new Trader { Id = 0, Name = "UEE" };
        public static Trader MargamanTrader = new Trader { Id = 0, Name = "Margaman" };

        public static List<Trader> AllTraders = new List<Trader>
        {
            UEETrader,
            MargamanTrader
        };

        public static Commodity AggricultruralSuppliesCommondity = new Commodity { Id = 0, Name = "Aggricultrural Supplies" };
        public static Commodity AgriciumCommondity = new Commodity { Id = 0, Name = "Agricium" };
        public static Commodity AluminiumCommondity = new Commodity { Id = 0, Name = "Aluminium" };
        public static Commodity AstatineCommondity = new Commodity { Id = 0, Name = "Astatine" };
        public static Commodity BerylCommondity = new Commodity { Id = 0, Name = "Beryl" };
        public static Commodity ChlorineCommondity = new Commodity { Id = 0, Name = "Chlorine" };
        public static Commodity CorundumCommondity = new Commodity { Id = 0, Name = "Corundum" };
        public static Commodity DiamondCommondity = new Commodity { Id = 0, Name = "Diamond" };
        public static Commodity DistilledSpiritsCommondity = new Commodity { Id = 0, Name = "Distilled Spirits" };
        public static Commodity FlourineCommondity = new Commodity { Id = 0, Name = "Flourine" };
        public static Commodity GoldCommondity = new Commodity { Id = 0, Name = "Gold" };
        public static Commodity HydrogenCommondity = new Commodity { Id = 0, Name = "Hydrogen" };
        public static Commodity IodineCommondity = new Commodity { Id = 0, Name = "Iodine" };
        public static Commodity LaraniteCommondity = new Commodity { Id = 0, Name = "Laranite" };
        public static Commodity MedicalSuppliesCommondity = new Commodity { Id = 0, Name = "Medical Supplies" };
        public static Commodity ProcessedFoodCommondity = new Commodity { Id = 0, Name = "Processed Food" };
        public static Commodity QuartzCommondity = new Commodity { Id = 0, Name = "Quartz" };
        public static Commodity RevenantTreePollenCommondity = new Commodity { Id = 0, Name = "Revenant Tree Pollen" };
        public static Commodity StimsCommondity = new Commodity { Id = 0, Name = "Stims" };
        public static Commodity TitaniumCommondity = new Commodity { Id = 0, Name = "Titanium" };
        public static Commodity TungstenCommondity = new Commodity { Id = 0, Name = "Tungsten" };
        public static Commodity AltruclatoxinCommondity = new Commodity { Id = 0, Name = "Altruclatoxin" };
        public static Commodity EtamCommondity = new Commodity { Id = 0, Name = @"E'tam" };
        public static Commodity SlamCommondity = new Commodity { Id = 0, Name = "SLAM" };
        public static Commodity NeonCommondity = new Commodity { Id = 0, Name = "Neon" };  
        public static Commodity WidowCommondity = new Commodity { Id = 0, Name = "WiDoW" };

        public static List<Commodity> AllCommodities = new List<Commodity>
        {
            AggricultruralSuppliesCommondity,
            AgriciumCommondity,
            AluminiumCommondity,
            AstatineCommondity,
            BerylCommondity,
            ChlorineCommondity,
            CorundumCommondity,
            DiamondCommondity,
            DistilledSpiritsCommondity,
            FlourineCommondity,
            GoldCommondity,
            HydrogenCommondity,
            IodineCommondity,
            LaraniteCommondity,
            MedicalSuppliesCommondity,
            ProcessedFoodCommondity,
            QuartzCommondity,
            RevenantTreePollenCommondity,
            StimsCommondity,
            TitaniumCommondity,
            TungstenCommondity,
            AltruclatoxinCommondity,
            EtamCommondity,
            SlamCommondity,
            NeonCommondity,
            WidowCommondity
        };
                
        public static AstralSystem GetStantonWithTradeData(IEnumerable<Commodity> commodities)
        {
            var system = TestSystemDataStatic.StantonSystem;
            var trades = GetTradesFromTestDataCsv(commodities, system);
            var planetBodies = system.Planets.Cast<AstralBody>();
            var moonBodies = system.Planets.SelectMany(p => p.Moons.Cast<AstralBody>());
            var bodies = planetBodies.Union(moonBodies);
            var tradePoints = system.TradePoints;

            foreach (var tradePoint in tradePoints)
            {
                if (trades.ContainsKey(tradePoint.Name))
                {
                    tradePoint.TradeOffers = trades[tradePoint.Name].ToList();
                }
            }

            return TestSystemDataStatic.StantonSystem;
        }

        private static IDictionary<string, List<TradeOffer>> GetTradesFromTestDataCsv(IEnumerable<Commodity> commodities, AstralSystem system)
        {
            var tradePoints = system.TradePoints.ToList();

            var buyRecords = File.ReadAllLines(@"./TestData/SCTradePrices - Buy.csv").Select(l => l.Split(','));
            var sellRecords = File.ReadAllLines(@"./TestData/SCTradePrices - Sell.csv").Select(l => l.Split(','));

            var groupByTradePointName = buyRecords.Union(sellRecords).GroupBy(r => r[0]);
            var tradesByLocation = groupByTradePointName.ToDictionary(g => g.Key, g => g.Select(r =>
            {
                return GetTradeFromRecord(r, tradePoints, commodities);
            }).ToList());

            return tradesByLocation;
        }

        private static TradeOffer GetTradeFromRecord(string[] record, IEnumerable<TradePoint> tradePoints, IEnumerable<Commodity> commodities)
        {
            var trade = new TradeOffer();
            trade.TradePoint = tradePoints.First(tp => tp.Name == record[0].Trim());
            trade.Commodity = commodities.First(gt => gt.Name == record[1].Trim());
            trade.PricePerUnit = Convert.ToDecimal(record[2].Trim());
            trade.DateTimeOfOffer = Convert.ToDateTime(record[3].Trim());
            trade.Buyer = AllTraders.First(t => t.Name == record[4].Trim());
            trade.Seller = AllTraders.First(t => t.Name == record[5].Trim());

            return trade;
        }
    }
}
