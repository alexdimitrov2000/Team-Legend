namespace TeamLegend.Services.Tests
{
    using Data;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using TeamLegend.Models;

    public class FixturesServiceTests
    {
        public FixturesServiceTests()
        {
            this.Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
        }

        public DbContextOptions<ApplicationDbContext> Options { get; }

        [Theory]
        [InlineData(0, "")]
        [InlineData(-10, "")]
        [InlineData(10, "")]
        [InlineData(0, null)]
        [InlineData(-10, null)]
        [InlineData(10, null)]
        [InlineData(10, "  ")]
        public async Task GetOrCreateAsync_WithInvalidParams_ReturnsNull(int fixtureRound, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            var result = await fixturesService.GetOrCreateAsync(fixtureRound, leagueId);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetOrCreateAsync_WithValidParamsAndEmptyContext_CreatesFixture(int round, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            var resultFixture = await fixturesService.GetOrCreateAsync(round, leagueId);

            Assert.Equal(1, context.Fixtures.Count());
            Assert.Equal(36, resultFixture.Id.Length);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetOrCreateAsync_WithValidParamsAndEmptyContext_CreatesFixtureWithGivenParams(int round, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            var resultFixture = await fixturesService.GetOrCreateAsync(round, leagueId);

            Assert.Equal(round, resultFixture.FixtureRound);
            Assert.Equal(leagueId, resultFixture.LeagueId);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetOrCreateAsync_WithExistingFixture_ReturnsExistingFixture(int round, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);
            var existingFixture = new Fixture { FixtureRound = round, LeagueId = leagueId };
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var resultFixture = await fixturesService.GetOrCreateAsync(round, leagueId);

            Assert.Equal(1, context.Fixtures.Count());
            Assert.Equal(existingFixture.Id, resultFixture.Id);
            Assert.Equal(existingFixture.LeagueId, resultFixture.LeagueId);
            Assert.Equal(existingFixture.FixtureRound, resultFixture.FixtureRound);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetOrCreateAsync_WithInconsistentRound_CreatesFixture(int round, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);
            var existingFixture = new Fixture { FixtureRound = round + 1, LeagueId = leagueId };
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var resultFixture = await fixturesService.GetOrCreateAsync(round, leagueId);

            Assert.Equal(2, context.Fixtures.Count());
            Assert.Equal(36, resultFixture.Id.Length);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetOrCreateAsync_WithInconsistentLeagueId_CreatesFixture(int round, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);
            var existingFixture = new Fixture { FixtureRound = round, LeagueId = leagueId + "new" };
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var resultFixture = await fixturesService.GetOrCreateAsync(round, leagueId);

            Assert.Equal(2, context.Fixtures.Count());
            Assert.Equal(36, resultFixture.Id.Length);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetOrCreateAsync_WithInconsistentParams_CreatesFixtureJustOnce(int round, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);
            var existingFixture = new Fixture { FixtureRound = round + 1, LeagueId = leagueId + "new" };
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var resultFixture = await fixturesService.GetOrCreateAsync(round, leagueId);

            Assert.Equal(2, context.Fixtures.Count());
            Assert.Equal(36, resultFixture.Id.Length);

            var secondCallFixture = await fixturesService.GetOrCreateAsync(round, leagueId);

            Assert.Equal(resultFixture.Id, secondCallFixture.Id);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(-10, "")]
        [InlineData(10, "")]
        [InlineData(5, "  ")]
        [InlineData(0, null)]
        [InlineData(-10, null)]
        public async Task GetByLeagueIdAndRoundAsync_WithInvalidParams_ReturnsNull(int fixtureRound, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            var result = await fixturesService.GetByLeagueIdAndRoundAsync(leagueId, fixtureRound);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetByLeagueIdAndRoundAsync_WithValidParamsAndEmptyContext_ReturnsNull(int fixtureRound, string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            var result = await fixturesService.GetByLeagueIdAndRoundAsync(leagueId, fixtureRound);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetByLeagueIdAndRoundAsync_WithInconsistentRound_ReturnsNull(int fixtureRound, string leagueId)
        {
            var existingFixture = new Fixture { FixtureRound = fixtureRound + 1, LeagueId = leagueId };
            var context = new ApplicationDbContext(this.Options);
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var result = await fixturesService.GetByLeagueIdAndRoundAsync(leagueId, fixtureRound);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetByLeagueIdAndRoundAsync_WithInconsistentLeagueId_ReturnsNull(int fixtureRound, string leagueId)
        {
            var existingFixture = new Fixture { FixtureRound = fixtureRound, LeagueId = leagueId + "new" };
            var context = new ApplicationDbContext(this.Options);
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var result = await fixturesService.GetByLeagueIdAndRoundAsync(leagueId, fixtureRound);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetByLeagueIdAndRoundAsync_WithInconsistentParams_ReturnsNull(int fixtureRound, string leagueId)
        {
            var existingFixture = new Fixture { FixtureRound = fixtureRound + 1, LeagueId = leagueId + "new" };
            var context = new ApplicationDbContext(this.Options);
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var result = await fixturesService.GetByLeagueIdAndRoundAsync(leagueId, fixtureRound);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "5")]
        [InlineData(10, "leagueId")]
        public async Task GetByLeagueIdAndRoundAsync_WithExistingFixture_ReturnsExistingFixture(int fixtureRound, string leagueId)
        {
            var existingFixture = new Fixture { FixtureRound = fixtureRound, LeagueId = leagueId };
            var context = new ApplicationDbContext(this.Options);
            await context.Fixtures.AddAsync(existingFixture);
            await context.SaveChangesAsync();

            var fixturesService = new FixturesService(context);

            var resultFixture = await fixturesService.GetByLeagueIdAndRoundAsync(leagueId, fixtureRound);

            Assert.NotNull(resultFixture);
            Assert.Equal(existingFixture.FixtureRound, resultFixture.FixtureRound);
            Assert.Equal(existingFixture.LeagueId, resultFixture.LeagueId);
            Assert.Equal(existingFixture.Id, resultFixture.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ValidateRoundAsync_WithInvalidLeagueId_ThrowsException(string leagueId)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            Assert.Throws<ArgumentException>(() => fixturesService.ValidateRoundAsync(leagueId, 1).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData("1", -1)]
        [InlineData("abc", -12)]
        [InlineData("leagueId", 0)]
        public async Task ValidateRoundAsync_WithNegativeOrZeroRound_ReturnsOne(string leagueId, int round)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            var fixture = new Fixture { LeagueId = leagueId, FixtureRound = 1 };
            await context.Fixtures.AddAsync(fixture);
            await context.SaveChangesAsync();

            var resultRound = await fixturesService.ValidateRoundAsync(leagueId, round);

            Assert.NotEqual(round, resultRound);
            Assert.Equal(1, resultRound);
        }

        [Theory]
        [InlineData("1", 15)]
        [InlineData("abc", 12)]
        [InlineData("leagueId", 20)]
        public async Task ValidateRoundAsync_WithHigherThanExistingRound_ReturnsLastExistingRound(string leagueId, int round)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);
            
            var fixtures = new Fixture[]
            {
                new Fixture { LeagueId = leagueId, FixtureRound = 1 },
                new Fixture { LeagueId = leagueId, FixtureRound = 3 },
                new Fixture { LeagueId = leagueId, FixtureRound = 5 },
            };
            await context.Fixtures.AddRangeAsync(fixtures);
            await context.SaveChangesAsync();

            var resultRound = await fixturesService.ValidateRoundAsync(leagueId, round);

            Assert.NotEqual(round, resultRound);
            Assert.Equal(5, resultRound);
        }

        [Theory]
        [InlineData("1", 15)]
        [InlineData("abc", 12)]
        [InlineData("leagueId", 20)]
        public async Task ValidateRoundAsync_WithNoExistingFixtures_ReturnsZero(string leagueId, int round)
        {
            var context = new ApplicationDbContext(this.Options);

            var fixturesService = new FixturesService(context);

            var resultRound = await fixturesService.ValidateRoundAsync(leagueId, round);

            Assert.NotEqual(round, resultRound);
            Assert.Equal(0, resultRound);
        }
    }
}
