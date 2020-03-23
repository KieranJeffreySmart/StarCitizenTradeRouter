using StarCitizenTradeRouter.Trading.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarCitizenTradeRouter.TraderTools.WebApp.Tests
{
    public static class TestSystemDataStatic
    {

        #region Crusader

        public static TradingPost ArcCorpMiningArea157Post = new TradingPost { Id = 0, Name = "ArcCorp Mining Area 157" };
        public static TradingPost BensonMiningOutpostPost = new TradingPost { Id = 0, Name = "Benson Mining Outpost" };
        public static TradingPost DeakinResearchOupostPost = new TradingPost { Id = 0, Name = "Deakin Research Oupost" };
        public static Moon YalaMoon = new Moon
        {
            Id = 0,
            Name = "Yala",
            TradePoints = new List<TradePoint>
            {
                ArcCorpMiningArea157Post,
                BensonMiningOutpostPost,
                DeakinResearchOupostPost
            }
        };

        public static TradingPost GalleteFamilyFarmPost = new TradingPost { Id = 0, Name = "Gallete Family Farm" };
        public static TradingPost HicksResearchOutpostPost = new TradingPost { Id = 0, Name = "Hicks Research Outpost" };
        public static TradingPost TerraMillsHydroFarmPost = new TradingPost { Id = 0, Name = "Terra Mills HydroFarm" };
        public static TradingPost TramAndMyersMiningPost = new TradingPost { Id = 0, Name = "Tram & Myers Mining" };

        public static Moon CellinMoon = new Moon
        {
            Id = 0,
            Name = "Cellin",
            TradePoints = new List<TradePoint>
            {
                GalleteFamilyFarmPost,
                HicksResearchOutpostPost,
                TerraMillsHydroFarmPost,
                TramAndMyersMiningPost
            }
        };

        public static TradingPost KudreOrePost = new TradingPost { Id = 0, Name = "Kudre Ore" };
        public static TradingPost ArcCorpMiningArea141Post = new TradingPost { Id = 0, Name = "ArcCorp Mining Area 141" };
        public static TradingPost ShubinMiningFacilitySCD1Post = new TradingPost { Id = 0, Name = "Shubin Mining Facility SCD 1" };
        public static TradingPost StashHousePost = new TradingPost { Id = 0, Name = "Stash House" };
        public static Moon DaymarMoon = new Moon
        {
            Id = 0,
            Name = "Daymar",
            TradePoints = new List<TradePoint>
            {
                KudreOrePost,
                ArcCorpMiningArea141Post,
                ShubinMiningFacilitySCD1Post,
                StashHousePost
            }
        };

        public static Port PortOlisarPost = new Port { Id = 0, Name = "Port Olisar" };
        public static Planet CrusaderPlanet = new Planet
        {
            Id = 0,
            Name = "Crusader",
            Moons = new List<Moon>
            {
                YalaMoon,
                CellinMoon,
                DaymarMoon
            },
            TradePoints = new List<TradePoint>
            {
                PortOlisarPost
            }
        };

        #endregion

        #region ArcCorp

        public static TradingPost LoveridgeMiningReservePost = new TradingPost { Id = 0, Name = "Loveridge Mining Reserve" };
        public static TradingPost ShubinMiningFacilitySAL2Post = new TradingPost { Id = 0, Name = "Shubin Mining Facility - SAL - 2" };
        public static TradingPost ShubinMiningFacilitySAL5Post = new TradingPost { Id = 0, Name = "Shubin Mining Facility - SAL - 5" };
        public static TradingPost TheOrphanagePost = new TradingPost { Id = 0, Name = "The Orphanage" };
        public static Moon LyriaMoon = new Moon
        {
            Id = 0,
            Name = "Lyria",
            TradePoints = new List<TradePoint>
            {
                LoveridgeMiningReservePost,
                ShubinMiningFacilitySAL2Post,
                ShubinMiningFacilitySAL5Post,
                TheOrphanagePost
            }
        };

        public static TradingPost ArcCorpMiningArea045Post = new TradingPost { Id = 0, Name = "ArcCorp Mining Area 045" };
        public static TradingPost ArcCorpMiningArea048Post = new TradingPost { Id = 0, Name = "ArcCorp Mining Area 048" };
        public static TradingPost ArcCorpMiningArea056Post = new TradingPost { Id = 0, Name = "ArcCorp Mining Area 056" };
        public static TradingPost ArcCorpMiningArea061Post = new TradingPost { Id = 0, Name = "ArcCorp Mining Area 061" };
        public static Moon WalaMoon = new Moon
        {
            Id = 0,
            Name = "Wala",
            TradePoints = new List<TradePoint>
            {
                ArcCorpMiningArea045Post,
                ArcCorpMiningArea048Post,
                ArcCorpMiningArea056Post,
                ArcCorpMiningArea061Post
            }
        };

        public static City Area18City = new City { Id = 0, Name = "Area 18" };
        public static Planet ArcCorpPlanet = new Planet
        {
            Id = 0,
            Name = "ArcCorp",
            Moons = new List<Moon>
            {
                LyriaMoon,
                WalaMoon
            },
            TradePoints = new List<TradePoint>
            {
                Area18City
            }
        };

        #endregion

        #region Hurston

        public static TradingPost HDMSAdamsPost = new TradingPost { Id = 0, Name = "HDMS Adams" };
        public static TradingPost HDMSNorgaardPost = new TradingPost { Id = 0, Name = "HDMS Norgaard" };
        public static Moon AberdeenMoon = new Moon
        {
            Id = 0,
            Name = "Aberdeen",
            TradePoints = new List<TradePoint>
            {
                HDMSAdamsPost,
                HDMSNorgaardPost
            }
        };

        public static TradingPost HDMSBezdekPost = new TradingPost { Id = 0, Name = "HDMS Bezdek" };
        public static TradingPost HDMSLatharPost = new TradingPost { Id = 0, Name = "HDMS Lathar" };
        public static Moon ArielMoon = new Moon
        {
            Id = 0,
            Name = "Ariel",
            TradePoints = new List<TradePoint>
            {
                HDMSBezdekPost,
                HDMSLatharPost
            }
        };

        public static TradingPost HDMSRyderPost = new TradingPost { Id = 0, Name = "HDMS Ryder" };
        public static TradingPost HDMSWoodrufPost = new TradingPost { Id = 0, Name = "HDMS Woodruf" };
        public static Moon ItaMoon = new Moon
        {
            Id = 0,
            Name = "Ita",
            TradePoints = new List<TradePoint>
            {
                HDMSRyderPost,
                HDMSWoodrufPost
            }
        };

        public static TradingPost HDMSHahalPost = new TradingPost { Id = 0, Name = "HDMS Hahal" };
        public static TradingPost HDMSPortmanPost = new TradingPost { Id = 0, Name = "HDMS Portman" };
        public static Moon MagdaMoon = new Moon
        {
            Id = 0,
            Name = "Magda",
            TradePoints = new List<TradePoint>
            {
                HDMSHahalPost,
                HDMSPortmanPost
            }
        };

        public static TradingPost HDMSEdmondPost = new TradingPost { Id = 0, Name = "HDMS Edmond" };
        public static TradingPost HDMSHadleyPost = new TradingPost { Id = 0, Name = "HDMS Hadley" };
        public static TradingPost HDMSPinewoodPost = new TradingPost { Id = 0, Name = "HDMS Pinewood" };
        public static City LorvileCity = new City { Id = 0, Name = "Lorvile" };
        public static Planet HurstonPlanet = new Planet
        {
            Id = 0,
            Name = "Hurston",
            Moons = new List<Moon>
            {
                AberdeenMoon,
                ArielMoon,
                ItaMoon,
                MagdaMoon
            },
            TradePoints = new List<TradePoint>
            {
                HDMSEdmondPost,
                HDMSHadleyPost,
                HDMSPinewoodPost,
                LorvileCity
            }
        };

        #endregion

        public static AstralSystem StantonSystem = new AstralSystem
        {
            Id = 0,
            Name = "Stanton",
            Planets = new List<Planet>
            {
                CrusaderPlanet,
                ArcCorpPlanet,
                HurstonPlanet
            }
        };
    }
}
