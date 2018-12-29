namespace TeamLegend.Services.Tests
{
    using TeamLegend.Data;
    using TeamLegend.Models;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class LeaguesServiceTests
    {
        public LeaguesServiceTests()
        {
            this.Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
        }

        public DbContextOptions<ApplicationDbContext> Options { get; }

        [Fact]
        public async Task CreateAsync_WithNullParam_ReturnsNullAndDoesNotCreate()
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var result = await leaguesService.CreateAsync(null);

            Assert.Equal(0, context.Leagues.Count());
            Assert.Null(result);
        }

        [Theory]
        [InlineData("1", "bg")]
        [InlineData("leagueName", "country")]
        public async Task CreateAsync_WithGivenName_CreatesLeague(string leagueName, string country)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League { Name = leagueName, Country = country };

            var result = await leaguesService.CreateAsync(league);

            Assert.Equal(1, context.Leagues.Count());
            Assert.Equal(36, result.Id.Length);
            Assert.Equal(leagueName, result.Name);
            Assert.Equal(country, result.Country);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("leagueName")]
        public async Task CreateAsync_WithGivenName_CreatesOnceThenThrows(string leagueName)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League { Name = leagueName };

            var result = await leaguesService.CreateAsync(league);

            Assert.Equal(1, context.Leagues.Count());
            Assert.Equal(36, result.Id.Length);

            Assert.Throws<ArgumentException>(() => leaguesService.CreateAsync(league).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllAsync_ReturnsAllCreatedLeagues(int leaguesCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            for (int i = 0; i < leaguesCount; i++)
            {
                await context.Leagues.AddAsync(new League());
            }

            await context.SaveChangesAsync();

            var result = await leaguesService.GetAllAsync();
            
            Assert.Equal(leaguesCount, result.Count);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetByIdAsync_WithInvalidIdParam_ReturnsNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var resultLeague = await leaguesService.GetByIdAsync(id);

            Assert.Null(resultLeague);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("leagueId", "leagueName")]
        public async Task GetByIdAsync_WithExistingLeague_ReturnsExistingLeague(string id, string leagueName)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var existingLeague = new League { Id = id, Name = leagueName };

            await context.Leagues.AddAsync(existingLeague);
            await context.SaveChangesAsync();

            var resultLeague = await leaguesService.GetByIdAsync(id);
            
            Assert.NotNull(resultLeague);
            Assert.Equal(existingLeague.Id, resultLeague.Id);
            Assert.Equal(existingLeague.Name, resultLeague.Name);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("leagueId", "leagueName")]
        public async Task GetByIdAsync_WithInexistentLeague_ReturnsNull(string id, string leagueName)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var existingLeague = new League { Id = id, Name = leagueName };

            var resultLeague = await leaguesService.GetByIdAsync(id);

            Assert.Null(resultLeague);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("leagueId", "leagueName")]
        public async Task GetByIdAsync_WithExistingLeagueButInconsistentId_ReturnsNull(string id, string leagueName)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var existingLeague = new League { Id = id, Name = leagueName };

            await context.Leagues.AddAsync(existingLeague);
            await context.SaveChangesAsync();

            var resultLeague = await leaguesService.GetByIdAsync(id + "null");

            Assert.Null(resultLeague);
        }

        [Fact]
        public async Task DeleteAsync_WithNullLeagueParam_ReturnsFalse()
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var isDeleteSuccessful = await leaguesService.DeleteAsync(null);

            Assert.False(isDeleteSuccessful);
        }

        [Fact]
        public async Task DeleteAsync_WithExistingLeagueParam_ReturnsTrue()
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League();
            await context.Leagues.AddAsync(league);
            await context.SaveChangesAsync();

            var isDeleteSuccessful = await leaguesService.DeleteAsync(league);

            Assert.True(isDeleteSuccessful);
            Assert.Equal(0, context.Leagues.Count());
        }

        [Fact]
        public async Task DeleteAsync_WithInxistentLeagueParam_ThrowsException()
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League { Name = "1" };

            await context.Leagues.AddAsync(league);
            await context.SaveChangesAsync();

            var leagueToDelete = new League { Name = "2" };

            Assert.Throws<DbUpdateConcurrencyException>(() => leaguesService.DeleteAsync(leagueToDelete).GetAwaiter().GetResult());
            Assert.Equal(1, context.Leagues.Count());
        }

        [Fact]
        public async Task AddTeamsAsync_WithInvalidParameters_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var result = await leaguesService.AddTeamsAsync(null, null);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddTeamsAsync_WithNullLeagueParameters_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var teams = new List<Team>();

            var result = await leaguesService.AddTeamsAsync(null, teams);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddTeamsAsync_WithNullTeamsParameters_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League();

            var result = await leaguesService.AddTeamsAsync(league, null);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task AddTeamsAsync_WithInexistentLeague_ThrowsException(int numberOfTeams)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League();
            await context.Leagues.AddAsync(league);

            var teams = new List<Team>();

            for (int i = 0; i < numberOfTeams; i++)
            {
                teams.Add(new Team());
            }

            Assert.Throws<DbUpdateConcurrencyException>(() => leaguesService.AddTeamsAsync(league, teams).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task AddTeamsAsync_WithExistingLeague_AddsTeamsToExistingLeague(int numberOfTeams)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League();
            var teams = new List<Team>();

            for (int i = 0; i < numberOfTeams; i++)
            {
                teams.Add(new Team());
            }

            var resultLeague = await leaguesService.AddTeamsAsync(league, teams);

            Assert.NotNull(resultLeague);
            Assert.Equal(numberOfTeams, resultLeague.Teams.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task AddTeamsAsync_WithExistingLeague_SetsTeamsLeague(int numberOfTeams)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League();

            await context.Leagues.AddAsync(league);
            await context.SaveChangesAsync();

            var teams = new List<Team>();

            for (int i = 0; i < numberOfTeams; i++)
            {
                teams.Add(new Team());
            }

            var resultLeague = await leaguesService.AddTeamsAsync(league, teams);

            Assert.NotNull(resultLeague);
            Assert.True(resultLeague.Teams.All(t => t.LeagueId == resultLeague.Id));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task AddTeamsAsync_WithExistingLeague_AddsToDb(int numberOfTeams)
        {
            var context = new ApplicationDbContext(this.Options);

            var leaguesService = new LeaguesService(context);

            var league = new League();

            var teams = new List<Team>();

            for (int i = 0; i < numberOfTeams; i++)
            {
                teams.Add(new Team());
            }

            var resultLeague = await leaguesService.AddTeamsAsync(league, teams);

            Assert.NotNull(resultLeague);
            Assert.Equal(1, context.Leagues.Count());
            Assert.Equal(numberOfTeams, context.Teams.Count());
        }
    }
}
