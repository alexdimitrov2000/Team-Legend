namespace TeamLegend.Services.Tests
{
    using Data;
    using TeamLegend.Models;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class MatchesServiceTests
    {
        public MatchesServiceTests()
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

            var matchesService = new MatchesService(context);

            var result = await matchesService.CreateAsync(null);

            Assert.Equal(0, context.Matches.Count());
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_WithHomeTeamAwayTeamAndFixture_CreatesMatch()
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var homeTeam = new Team();
            var awayTeam = new Team();
            var fixture = new Fixture();

            await context.Teams.AddAsync(homeTeam);
            await context.Teams.AddAsync(awayTeam);
            await context.Fixtures.AddAsync(fixture);
            await context.SaveChangesAsync();

            var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam, Fixture = fixture };

            var resultMatch = await matchesService.CreateAsync(match);

            Assert.NotNull(resultMatch);
            Assert.Equal(1, context.Matches.Count());
            Assert.Equal(36, resultMatch.Id.Length);
            Assert.Equal(homeTeam.Id, resultMatch.HomeTeamId);
            Assert.Equal(awayTeam.Id, resultMatch.AwayTeamId);
            Assert.Equal(fixture.Id, resultMatch.FixtureId);
        }

        [Fact]
        public async Task CreateAsync_WithInexistentTeamsAndFixture_CreatesMatchAndAddsEntitiesToContext()
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var homeTeam = new Team();
            var awayTeam = new Team();
            var fixture = new Fixture();

            var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam, Fixture = fixture };

            var resultMatch = await matchesService.CreateAsync(match);

            Assert.NotNull(resultMatch);
            Assert.Equal(1, context.Matches.Count());
            Assert.Equal(36, resultMatch.Id.Length);
            Assert.Equal(36, resultMatch.HomeTeamId.Length);
            Assert.Equal(36, resultMatch.AwayTeamId.Length);
            Assert.Equal(36, resultMatch.FixtureId.Length);
            Assert.Equal(homeTeam.Id, resultMatch.HomeTeamId);
            Assert.Equal(awayTeam.Id, resultMatch.AwayTeamId);
            Assert.Equal(fixture.Id, resultMatch.FixtureId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllPlayedAsync_ReturnsAllCreatedPlayedMatches(int numberOfMatches)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var matches = new List<Match>();

            for (int i = 0; i < numberOfMatches; i++)
            {
                matches.Add(new Match { IsPlayed = false });
                matches.Add(new Match { IsPlayed = true });
            }

            await context.Matches.AddRangeAsync(matches);
            await context.SaveChangesAsync();

            // two not created matches
            matches.Add(new Match { IsPlayed = false });
            matches.Add(new Match { IsPlayed = true });

            var result = await matchesService.GetAllPlayedAsync();

            Assert.NotNull(result);
            Assert.Equal(numberOfMatches * 2, context.Matches.Count());
            Assert.Equal(numberOfMatches, result.Count());
            Assert.True(result.All(m => m.IsPlayed));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllUnplayedAsync_ReturnsAllCreatedUnplayedMatches(int numberOfMatches)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var matches = new List<Match>();

            for (int i = 0; i < numberOfMatches; i++)
            {
                matches.Add(new Match { IsPlayed = false });
                matches.Add(new Match { IsPlayed = true });
            }

            await context.Matches.AddRangeAsync(matches);
            await context.SaveChangesAsync();

            // two not created matches
            matches.Add(new Match { IsPlayed = false });
            matches.Add(new Match { IsPlayed = true });

            var result = await matchesService.GetAllUnplayedAsync();

            Assert.NotNull(result);
            Assert.Equal(numberOfMatches * 2, context.Matches.Count());
            Assert.Equal(numberOfMatches, result.Count());
            Assert.True(result.All(m => !m.IsPlayed));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetByIdAsync_WithInvalidIdParam_ReturnsNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var resultMatch = await matchesService.GetByIdAsync(id);

            Assert.Null(resultMatch);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "asd")]
        [InlineData("matchId", "fixtureId")]
        public async Task GetByIdAsync_WithExistingMatch_ReturnsExistingMatch(string id, string fixtureId)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var existingMatch = new Match { Id = id, FixtureId = fixtureId };

            await context.Matches.AddAsync(existingMatch);
            await context.SaveChangesAsync();

            var resultMatch = await matchesService.GetByIdAsync(id);

            Assert.NotNull(resultMatch);
            Assert.Equal(existingMatch.Id, resultMatch.Id);
            Assert.Equal(existingMatch.FixtureId, resultMatch.FixtureId);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("abc")]
        [InlineData("matchId")]
        public async Task GetByIdAsync_WithInexistentMatchReturnsNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var inexistentMatch = new Match { Id = id };

            var resultMatch = await matchesService.GetByIdAsync(id);

            Assert.Null(resultMatch);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("abc")]
        [InlineData("matchId")]
        public async Task GetByIdAsync_WithExistingMatchButInconsistentId_ReturnsExistingNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var existingMatch = new Match { Id = id };

            await context.Matches.AddAsync(existingMatch);
            await context.SaveChangesAsync();

            var resultMatch = await matchesService.GetByIdAsync(id + "null");

            Assert.Null(resultMatch);
        }

        [Fact]
        public async Task UpdateTeamsGoalsAsync_WithInvalidParameters_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var match = new Match();

            var firstResult = await matchesService.UpdateTeamsGoalsAsync(null, -1, -1);
            var secondResult = await matchesService.UpdateTeamsGoalsAsync(match, -1, -1);
            var thirdResult = await matchesService.UpdateTeamsGoalsAsync(null, 0, -1);
            var fourthResult = await matchesService.UpdateTeamsGoalsAsync(null, -1, 2);
            var fifthResult = await matchesService.UpdateTeamsGoalsAsync(match, -1, 2);

            Assert.Null(firstResult);
            Assert.Null(secondResult);
            Assert.Null(thirdResult);
            Assert.Null(fourthResult);
            Assert.Null(fifthResult);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        [InlineData(3, 3)]
        [InlineData(6, 5)]
        public async Task UpdateTeamsGoalsAsync_WithExistingMatchAndTeams_UpdatesTeamsGoals(int homeTeamGoals, int awayTeamGoals)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var homeTeam = new Team();
            var awayTeam = new Team();
            var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };

            await context.Teams.AddAsync(homeTeam);
            await context.Teams.AddAsync(awayTeam);
            await context.Matches.AddAsync(match);
            await context.SaveChangesAsync();

            var resultMatch = await matchesService.UpdateTeamsGoalsAsync(match, homeTeamGoals, awayTeamGoals);

            Assert.NotNull(resultMatch);
            Assert.Equal(36, resultMatch.HomeTeamId.Length);
            Assert.Equal(36, resultMatch.AwayTeamId.Length);
            Assert.Equal(homeTeam.Id, resultMatch.HomeTeamId);
            Assert.Equal(awayTeam.Id, resultMatch.AwayTeamId);
            Assert.Equal(homeTeamGoals, resultMatch.HomeTeam.GoalsScored);
            Assert.Equal(awayTeamGoals, resultMatch.HomeTeam.GoalsConceded);
            Assert.Equal(awayTeamGoals, resultMatch.AwayTeam.GoalsScored);
            Assert.Equal(homeTeamGoals, resultMatch.AwayTeam.GoalsConceded);
            Assert.Equal(0, resultMatch.HomeTeamGoals);
            Assert.Equal(0, resultMatch.AwayTeamGoals);
            Assert.False(resultMatch.IsPlayed);
        }

        [Fact]
        public async Task UpdateScoreAsync_WithInvalidParams_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var match = new Match();

            var firstResult = await matchesService.UpdateScoreAsync(null, -1, -1);
            var secondResult = await matchesService.UpdateScoreAsync(match, -1, -1);
            var thirdResult = await matchesService.UpdateScoreAsync(null, 0, -1);
            var fourthResult = await matchesService.UpdateScoreAsync(null, -1, 2);
            var fifthResult = await matchesService.UpdateScoreAsync(match, -1, 2);

            Assert.Null(firstResult);
            Assert.Null(secondResult);
            Assert.Null(thirdResult);
            Assert.Null(fourthResult);
            Assert.Null(fifthResult);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        [InlineData(3, 3)]
        [InlineData(6, 5)]
        public async Task UpdateScoreAsync_WithExistingMatchAndTeams_UpdatesMatchScore(int homeTeamGoals, int awayTeamGoals)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var homeTeam = new Team();
            var awayTeam = new Team();
            var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };

            await context.Teams.AddAsync(homeTeam);
            await context.Teams.AddAsync(awayTeam);
            await context.Matches.AddAsync(match);
            await context.SaveChangesAsync();

            var resultMatch = await matchesService.UpdateScoreAsync(match, homeTeamGoals, awayTeamGoals);

            Assert.NotNull(resultMatch);
            Assert.Equal(36, resultMatch.HomeTeamId.Length);
            Assert.Equal(36, resultMatch.AwayTeamId.Length);
            Assert.Equal(homeTeam.Id, resultMatch.HomeTeamId);
            Assert.Equal(awayTeam.Id, resultMatch.AwayTeamId);
            Assert.Equal(0, resultMatch.HomeTeam.GoalsScored);
            Assert.Equal(0, resultMatch.HomeTeam.GoalsConceded);
            Assert.Equal(0, resultMatch.AwayTeam.GoalsScored);
            Assert.Equal(0, resultMatch.AwayTeam.GoalsConceded);
            Assert.Equal(homeTeamGoals, resultMatch.HomeTeamGoals);
            Assert.Equal(awayTeamGoals, resultMatch.AwayTeamGoals);
            Assert.True(resultMatch.IsPlayed);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(6, 5)]
        public async Task UpdateScoreAsync_WithMoreHomeTeamGoals_IncreasesHomeTeamPointsByThree(int homeTeamGoals, int awayTeamGoals)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var homeTeam = new Team();
            var awayTeam = new Team();
            var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };

            await context.Teams.AddAsync(homeTeam);
            await context.Teams.AddAsync(awayTeam);
            await context.Matches.AddAsync(match);
            await context.SaveChangesAsync();

            var resultMatch = await matchesService.UpdateScoreAsync(match, homeTeamGoals, awayTeamGoals);

            Assert.NotNull(resultMatch);
            Assert.Equal(3, resultMatch.HomeTeam.TotalPoints);
            Assert.Equal(0, resultMatch.AwayTeam.TotalPoints);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(6, 5)]
        public async Task UpdateScoreAsync_WithMoreAwayTeamGoals_IncreasesAwayTeamPointsByThree(int awayTeamGoals, int homeTeamGoals)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var homeTeam = new Team();
            var awayTeam = new Team();
            var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };

            await context.Teams.AddAsync(homeTeam);
            await context.Teams.AddAsync(awayTeam);
            await context.Matches.AddAsync(match);
            await context.SaveChangesAsync();

            var resultMatch = await matchesService.UpdateScoreAsync(match, homeTeamGoals, awayTeamGoals);

            Assert.NotNull(resultMatch);
            Assert.Equal(0, resultMatch.HomeTeam.TotalPoints);
            Assert.Equal(3, resultMatch.AwayTeam.TotalPoints);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(6, 6)]
        public async Task UpdateScoreAsync_WithEqualTeamsGoals_IncreasesTeamsPointsByOne(int awayTeamGoals, int homeTeamGoals)
        {
            var context = new ApplicationDbContext(this.Options);

            var matchesService = new MatchesService(context);

            var homeTeam = new Team();
            var awayTeam = new Team();
            var match = new Match { HomeTeam = homeTeam, AwayTeam = awayTeam };

            await context.Teams.AddAsync(homeTeam);
            await context.Teams.AddAsync(awayTeam);
            await context.Matches.AddAsync(match);
            await context.SaveChangesAsync();

            var resultMatch = await matchesService.UpdateScoreAsync(match, homeTeamGoals, awayTeamGoals);

            Assert.NotNull(resultMatch);
            Assert.Equal(1, resultMatch.HomeTeam.TotalPoints);
            Assert.Equal(1, resultMatch.AwayTeam.TotalPoints);
        }
    }
}
