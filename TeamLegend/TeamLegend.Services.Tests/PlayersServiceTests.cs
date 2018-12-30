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

    public class PlayersServiceTests
    {
        public PlayersServiceTests()
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

            var playersService = new PlayersService(context);

            var result = await playersService.CreateAsync(null);

            Assert.Equal(0, context.Players.Count());
            Assert.Null(result);
        }

        [Theory]
        [InlineData("1", "bg")]
        [InlineData("playerName", "nationality")]
        public async Task CreateAsync_WithGivenNameAndNationality_CreatesPlayer(string playerName, string nationality)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var player = new Player { Name = playerName, Nationality = nationality };

            var resultPlayer = await playersService.CreateAsync(player);

            Assert.Equal(1, context.Players.Count());
            Assert.Equal(36, resultPlayer.Id.Length);
            Assert.Equal(playerName, resultPlayer.Name);
            Assert.Equal(nationality, resultPlayer.Nationality);
            Assert.Equal(player.Name, resultPlayer.Name);
            Assert.Equal(player.Nationality, resultPlayer.Nationality);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetByIdAsync_WithInvalidIdParam_ReturnsNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var resultPlayer = await playersService.GetByIdAsync(id);

            Assert.Null(resultPlayer);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("playerId", "playerName")]
        public async Task GetByIdAsync_WithExistingPlayer_ReturnsExistingPlayer(string id, string playerName)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var existingPlayer = new Player { Id = id, Name = playerName };

            await context.Players.AddAsync(existingPlayer);
            await context.SaveChangesAsync();

            var resultPlayer = await playersService.GetByIdAsync(id);

            Assert.NotNull(resultPlayer);
            Assert.Equal(existingPlayer.Id, resultPlayer.Id);
            Assert.Equal(existingPlayer.Name, resultPlayer.Name);
            Assert.Equal(id, resultPlayer.Id);
            Assert.Equal(playerName, resultPlayer.Name);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("playerId", "playerName")]
        public async Task GetByIdAsync_WithInexistentPlayer_ReturnsNull(string id, string playerName)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var inexistentPlayer = new Player { Id = id, Name = playerName };

            var resultPlayer = await playersService.GetByIdAsync(id);

            Assert.Null(resultPlayer);
            Assert.Equal(0, context.Players.Count());
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("playerId", "playerName")]
        public async Task GetByIdAsync_WithExistingPlayerButInconsistentId_ReturnsNull(string id, string playerName)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var existingPlayer = new Player { Id = id, Name = playerName };

            await context.Players.AddAsync(existingPlayer);
            await context.SaveChangesAsync();

            var resultPlayer = await playersService.GetByIdAsync(id + "null");

            Assert.Null(resultPlayer);
            Assert.Equal(1, context.Players.Count());
        }

        [Fact]
        public async Task DeleteAsync_WithNullPlayerParam_ReturnsFalse()
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var isDeleteSuccessful = await playersService.DeleteAsync(null);

            Assert.False(isDeleteSuccessful);
        }

        [Fact]
        public async Task DeleteAsync_WithExistingPlayerParam_ReturnsTrue()
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var player = new Player();
            await context.Players.AddAsync(player);
            await context.SaveChangesAsync();

            var isDeleteSuccessful = await playersService.DeleteAsync(player);

            Assert.True(isDeleteSuccessful);
            Assert.Equal(0, context.Players.Count());
        }

        [Theory]
        [InlineData("1")]
        [InlineData("asd")]
        [InlineData("playerName")]
        public async Task DeleteAsync_WithInxistentPlayerParam_ThrowsException(string name)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var player = new Player { Name = name };

            await context.Players.AddAsync(player);
            await context.SaveChangesAsync();

            var playerToDelete = new Player { Name = name + "diff" };

            Assert.Throws<DbUpdateConcurrencyException>(() => playersService.DeleteAsync(playerToDelete).GetAwaiter().GetResult());
            Assert.Equal(1, context.Players.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllAsync_ReturnsAllCreatedPlayers(int playersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            for (int i = 0; i < playersCount; i++)
                await context.Players.AddAsync(new Player { Name = "Pesho" });

            await context.SaveChangesAsync();

            var result = await playersService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(playersCount, result.Count);
            Assert.True(result.All(p => p.Name == "Pesho"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllWithoutTeamAsync_ReturnsAllPlayersWithoutTeam(int playersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            for (int i = 0; i < playersCount; i++)
                await context.Players.AddAsync(new Player { Name = "Pesho", CurrentTeamId = "1" });

            for (int i = 0; i < playersCount; i++)
                await context.Players.AddAsync(new Player { Name = "Gosho" });

            await context.SaveChangesAsync();

            var result = await playersService.GetAllWithoutTeamAsync();

            Assert.NotNull(result);
            Assert.Equal(playersCount * 2, context.Players.Count());
            Assert.Equal(playersCount, result.Count);
            Assert.True(result.All(p => p.Name == "Gosho"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetAllFromTeamAsync_WithInvalidTeamId_ReturnsNull(string teamId)
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var result = await playersService.GetAllFromTeamAsync(teamId);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("teamId", 5)]
        public async Task GetAllFromTeamAsync_WithInexistentTeam_ReturnsEmptyList(string teamId, int playersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var team = new Team { Id = teamId };

            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            for (int i = 0; i < playersCount; i++)
                await context.Players.AddAsync(new Player { CurrentTeam = team });

            await context.SaveChangesAsync();

            var playersService = new PlayersService(context);

            var result = await playersService.GetAllFromTeamAsync(teamId + "new");

            Assert.Equal(1, context.Teams.Count());
            Assert.Equal(playersCount, context.Players.Count());
            Assert.Equal(0, result.Count);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("teamId", 5)]
        public async Task GetAllFromTeamAsync_ReturnsAllPlayersFromThatTeam(string teamId, int playersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var team = new Team { Id = teamId };

            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            for (int i = 0; i < playersCount; i++)
                await context.Players.AddAsync(new Player { Name = "Pesho", CurrentTeam = team });

            for (int i = 0; i < playersCount; i++)
                await context.Players.AddAsync(new Player { Name = "Gosho", CurrentTeamId = teamId + "new" });

            await context.SaveChangesAsync();

            var playersService = new PlayersService(context);

            var result = await playersService.GetAllFromTeamAsync(teamId);

            Assert.NotNull(result);
            Assert.Equal(1, context.Teams.Count());
            Assert.Equal(playersCount * 2, context.Players.Count());
            Assert.Equal(playersCount, result.Count);
            Assert.True(result.All(p => p.Name == "Pesho"));
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("teamId", 5)]
        public async Task GetAllFromTeamAsync_WithTwoExistingTeams_ReturnsAllPlayersFromTeamWithId(string teamId, int playersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var firstTeam = new Team { Id = teamId };
            var secondTeam = new Team { Id = teamId + "new" };

            await context.Teams.AddAsync(firstTeam);
            await context.Teams.AddAsync(secondTeam);
            await context.SaveChangesAsync();

            for (int i = 0; i < playersCount; i++)
            {
                await context.Players.AddAsync(new Player { Name = "Pesho", CurrentTeam = firstTeam });
                await context.Players.AddAsync(new Player { Name = "Gosho", CurrentTeam = secondTeam });
            }

            await context.SaveChangesAsync();

            var playersService = new PlayersService(context);

            var result = await playersService.GetAllFromTeamAsync(teamId);

            Assert.NotNull(result);
            Assert.Equal(2, context.Teams.Count());
            Assert.Equal(playersCount * 2, context.Players.Count());
            Assert.Equal(playersCount, result.Count);
            Assert.True(result.All(p => p.Name == "Pesho"));
        }

        [Fact]
        public async Task IncreasePlayersGoalsAsync_WithNullPlayersParameter_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var playersService = new PlayersService(context);

            var result = await playersService.IncreasePlayersGoalsAsync(null);

            Assert.Null(result);
        }

        [Fact]
        public async Task IncreasePlayersGoalsAsync_WithExistingPlayers_IncreasesPlayersGoalsAndReturnsList()
        {
            var context = new ApplicationDbContext(this.Options);

            var players = new List<Player>
            {
                new Player(),
                new Player(),
                new Player(),
                new Player(),
            };

            await context.Players.AddRangeAsync(players);
            await context.SaveChangesAsync();

            var playersService = new PlayersService(context);

            var result = await playersService.IncreasePlayersGoalsAsync(players);

            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
            Assert.True(result.All(p => p.GoalsScored == 1));
        }

        [Fact]
        public async Task IncreasePlayersGoalsAsync_WithInxistentPlayers_ThrowsException()
        {
            var context = new ApplicationDbContext(this.Options);

            var players = new List<Player>
            {
                new Player(),
                new Player(),
                new Player(),
                new Player(),
            };

            await context.Players.AddRangeAsync(players);

            var playersService = new PlayersService(context);

            Assert.Equal(0, context.Players.Count());
            Assert.Throws<DbUpdateConcurrencyException>(() => playersService.IncreasePlayersGoalsAsync(players).GetAwaiter().GetResult());
        }
    }
}
