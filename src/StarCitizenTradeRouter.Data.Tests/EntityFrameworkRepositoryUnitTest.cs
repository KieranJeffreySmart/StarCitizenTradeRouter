using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StarCitizenTradeRouter.Services;
using System.Threading.Tasks;
using Xunit;

namespace StarCitizenTradeRouter.Data.Tests
{
    public class EntityFrameworkRepositoryUnitTest
    {
        [Fact]
        public async Task TestNew()
        {
            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase("Test1")
                .Options;

            var context = new TestContext(options);
            ISimpleRepository<int, TestEntity> sut = new EntityFrameworkRepository<int, TestEntity>(context);

            var entity = new TestEntity { Name = "TestEntity1" };
            await sut.New(entity);
            
            entity.Id.Should().BeGreaterThan(0);
            (await context.FindAsync<TestEntity>(entity.Id)).Should().NotBeNull();
        }

        [Fact]
        public async Task TestGet()
        {
            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase("Test1")
                .Options;

            var context = new TestContext(options);
            ISimpleRepository<int, TestEntity> sut = new EntityFrameworkRepository<int, TestEntity>(context);
            var entity = new TestEntity { Name = "TestEntity1" };
            context.Add(entity);
            entity.Id.Should().BeGreaterThan(0);
            
            var actualEntity = await sut.Get(entity.Id);

            actualEntity.Should().NotBeNull();
        }
    }

    public class TestContext : DbContext
    {
        public DbSet<TestEntity> TestEntities;

        public TestContext()
        {
        }

        public TestContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>().HasKey(t => t.Id);
        }
    }

    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
