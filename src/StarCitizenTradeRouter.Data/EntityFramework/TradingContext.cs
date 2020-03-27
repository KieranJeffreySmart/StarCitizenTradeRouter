using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Trading.Dtos;

namespace StarCitizenTradeRouter.Data.EntityFramework
{
    public class TradingContext : DbContext
    {
        public DbSet<Trader> Traders;
        public DbSet<AstralSystem> Systems;
        public DbSet<AstralBody> AstralBodies;
        public DbSet<Planet> Planets;
        public DbSet<Moon> Moons;
        public DbSet<TradePoint> TradePoints;
        public DbSet<TradingPost> TradingPosts;
        public DbSet<Port> Ports;
        public DbSet<City> Cities;
        public DbSet<Commodity> Commodoties;
        public DbSet<TradeOffer> TradeOffers;

        public TradingContext(DbContextOptions<TradingContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trader>().HasKey(t => t.Id);
            modelBuilder.Entity<AstralSystem>().Ignore(ab => ab.TradePoints).HasKey(t => t.Id);
            modelBuilder.Entity<AstralBody>().HasKey(t => t.Id);
            modelBuilder.Entity<Planet>();
            modelBuilder.Entity<Moon>();
            modelBuilder.Entity<TradePoint>().HasKey(t => t.Id);
            modelBuilder.Entity<TradingPost>();
            modelBuilder.Entity<Port>();
            modelBuilder.Entity<City>();
            modelBuilder.Entity<Commodity>().HasKey(t => t.Id);
            modelBuilder.Entity<TradeOffer>().HasKey(t => t.Id);
        }
    }
}
