namespace TeamLegend.Services.Tests
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TeamLegend.Models;
    using Xunit;

    public class TeamsServiceTests
    {
        public TeamsServiceTests()
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

            var teamsService = new TeamsService(context);

            var result = await teamsService.CreateAsync(null);

            Assert.Equal(0, context.Teams.Count());
            Assert.Null(result);
        }

        [Theory]
        [InlineData("1", "bg")]
        [InlineData("teamName", "nickname")]
        public async Task CreateAsync_WithGivenNameAndNationality_CreatesTeam(string teamName, string nickname)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team { Name = teamName, Nickname = nickname };

            var resultTeam = await teamsService.CreateAsync(team);

            Assert.Equal(1, context.Teams.Count());
            Assert.Equal(36, resultTeam.Id.Length);
            Assert.Equal(teamName, resultTeam.Name);
            Assert.Equal(nickname, resultTeam.Nickname);
            Assert.Equal(team.Name, resultTeam.Name);
            Assert.Equal(team.Nickname, resultTeam.Nickname);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetByIdAsync_WithInvalidIdParam_ReturnsNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var resultTeam = await teamsService.GetByIdAsync(id);

            Assert.Null(resultTeam);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("teamId", "teamName")]
        public async Task GetByIdAsync_WithExistingTeam_ReturnsExistingTeam(string id, string teamName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var existingTeam = new Team { Id = id, Name = teamName };

            await context.Teams.AddAsync(existingTeam);
            await context.SaveChangesAsync();

            var resultTeam = await teamsService.GetByIdAsync(id);

            Assert.NotNull(resultTeam);
            Assert.Equal(existingTeam.Id, resultTeam.Id);
            Assert.Equal(existingTeam.Name, resultTeam.Name);
            Assert.Equal(id, resultTeam.Id);
            Assert.Equal(teamName, resultTeam.Name);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("teamId", "teamName")]
        public async Task GetByIdAsync_WithInexistentTeam_ReturnsNull(string id, string teamName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var inexistentTeam = new Team { Id = id, Name = teamName };

            var resultTeam = await teamsService.GetByIdAsync(id);

            Assert.Null(resultTeam);
            Assert.Equal(0, context.Teams.Count());
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("teamId", "teamName")]
        public async Task GetByIdAsync_WithExistingTeamButInconsistentId_ReturnsNull(string id, string teamName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var existingTeam = new Team { Id = id, Name = teamName };

            await context.Teams.AddAsync(existingTeam);
            await context.SaveChangesAsync();

            var resultTeam = await teamsService.GetByIdAsync(id + "null");

            Assert.Null(resultTeam);
            Assert.Equal(1, context.Teams.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllAsync_ReturnsAllCreatedTeams(int teamsCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            for (int i = 0; i < teamsCount; i++)
                await context.Teams.AddAsync(new Team { Name = "Team" });

            await context.SaveChangesAsync();

            var result = await teamsService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(teamsCount, result.Count);
            Assert.True(result.All(p => p.Name == "Team"));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllWithoutLeagueAsync_ReturnsAllTeamsWithoutTeam(int teamsCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            for (int i = 0; i < teamsCount; i++)
            {
                await context.Teams.AddAsync(new Team { Name = "Team1", LeagueId = "1" });
                await context.Teams.AddAsync(new Team { Name = "Team2" });
            }

            await context.SaveChangesAsync();

            var result = await teamsService.GetAllWithoutLeagueAsync();

            Assert.NotNull(result);
            Assert.Equal(teamsCount * 2, context.Teams.Count());
            Assert.Equal(teamsCount, result.Count);
            Assert.True(result.All(p => p.Name == "Team2"));
        }

        [Fact]
        public async Task DeleteAsync_WithNullTeamParam_ReturnsFalse()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var isDeleteSuccessful = await teamsService.DeleteAsync(null);

            Assert.False(isDeleteSuccessful);
        }

        [Fact]
        public async Task DeleteAsync_WithExistingTeamParam_ReturnsTrue()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();
            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            Assert.Equal(1, context.Teams.Count());

            var isDeleteSuccessful = await teamsService.DeleteAsync(team);

            Assert.True(isDeleteSuccessful);
            Assert.Equal(0, context.Teams.Count());
        }

        [Theory]
        [InlineData("1")]
        [InlineData("asd")]
        [InlineData("teamName")]
        public async Task DeleteAsync_WithInxistentTeamParam_ThrowsException(string name)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team { Name = name };

            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            Assert.Equal(1, context.Teams.Count());

            var teamToDelete = new Team { Name = name + "diff" };

            Assert.Throws<DbUpdateConcurrencyException>(() => teamsService.DeleteAsync(teamToDelete).GetAwaiter().GetResult());
            Assert.Equal(1, context.Teams.Count());
        }

        [Fact]
        public async Task SetStadiumAsync_WithNullParameters_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();
            var stadium = new Stadium();

            var firstResult = await teamsService.SetStadiumAsync(null, null);
            var secondResult = await teamsService.SetStadiumAsync(team, null);
            var thirdResult = await teamsService.SetStadiumAsync(null, stadium);

            Assert.Null(firstResult);
            Assert.Null(secondResult);
            Assert.Null(thirdResult);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "asd")]
        [InlineData("teamName", "stadiumName")]
        public async Task SetStadiumAsync_WithExistingTeamAndStadium_SetsStadiumToTeam(string teamName, string stadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team { Name = teamName };
            var stadium = new Stadium { Name = stadiumName };

            await context.Teams.AddAsync(team);
            await context.Stadiums.AddAsync(stadium);

            await context.SaveChangesAsync();

            var resultTeam = await teamsService.SetStadiumAsync(team, stadium);

            Assert.NotNull(resultTeam);
            Assert.Equal(36, resultTeam.Id.Length);
            Assert.Equal(team.Id, resultTeam.Id);
            Assert.Equal(stadium.Id, resultTeam.StadiumId);
            Assert.Equal(team.Name, resultTeam.Name);
            Assert.Equal(stadium.Name, resultTeam.Stadium.Name);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "asd")]
        [InlineData("teamName", "stadiumName")]
        public async Task SetStadiumAsync_WithExistingTeamAndInexistentStadium_CreatesStadiumAndSetsItToTeam(string teamName, string stadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team { Name = teamName };
            var stadium = new Stadium { Name = stadiumName };

            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            await context.Stadiums.AddAsync(stadium);

            var resultTeam = await teamsService.SetStadiumAsync(team, stadium);

            Assert.NotNull(resultTeam);
            Assert.Equal(1, context.Stadiums.Count());
            Assert.Equal(36, resultTeam.Id.Length);
            Assert.Equal(team.Id, resultTeam.Id);
            Assert.Equal(stadium.Id, resultTeam.StadiumId);
            Assert.Equal(team.Name, resultTeam.Name);
            Assert.Equal(stadium.Name, resultTeam.Stadium.Name);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "asd")]
        [InlineData("teamName", "stadiumName")]
        public async Task SetStadiumAsync_WithExistingStadiumAndInexistentTeam_ThrowsException(string teamName, string stadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team { Name = teamName };
            var stadium = new Stadium { Name = stadiumName };
            
            await context.Stadiums.AddAsync(stadium);
            await context.SaveChangesAsync();

            await context.Teams.AddAsync(team);
            
            Assert.Equal(1, context.Stadiums.Count());
            Assert.Equal(0, context.Teams.Count());
            Assert.Throws<DbUpdateConcurrencyException>(() => teamsService.SetStadiumAsync(team, stadium).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData("1", "2", "3")]
        [InlineData("abc", "asd", "abv")]
        [InlineData("teamName", "setStadiumName", "newStadiumName")]
        public async Task SetStadiumAsync_WithSetStadiumToTeam_SetsNewStadium(string teamName, string setStadiumName, string newStadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var setStadium = new Stadium { Name = setStadiumName };
            var team = new Team { Name = teamName, Stadium = setStadium };
            var newStadium = new Stadium { Name = newStadiumName };

            await context.Stadiums.AddAsync(setStadium);
            await context.Stadiums.AddAsync(newStadium);
            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            var setNewStadiumResult = await teamsService.SetStadiumAsync(team, newStadium);

            Assert.Equal(2, context.Stadiums.Count());
            Assert.Equal(1, context.Teams.Count());
            Assert.NotNull(setNewStadiumResult);
            Assert.Equal(newStadium.Id, setNewStadiumResult.StadiumId);
            Assert.Equal(newStadium.Name, setNewStadiumResult.Stadium.Name);
            Assert.Equal(team.Id, setNewStadiumResult.Id);
            Assert.Equal(team.Name, setNewStadiumResult.Name);
        }

        [Fact]
        public async Task AddNewPlayersAsync_WithNullParameters_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();
            var players = new List<Player>();

            var firstResult = await teamsService.AddNewPlayersAsync(null, null);
            var secondResult = await teamsService.AddNewPlayersAsync(team, null);
            var thirdResult = await teamsService.AddNewPlayersAsync(null, players);

            Assert.Null(firstResult);
            Assert.Null(secondResult);
            Assert.Null(thirdResult);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task AddNewPlayersAsync_WithInexistentTeam_ThrowsException(int playersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var players = new List<Player>();

            for (int i = 0; i < playersCount; i++)
                players.Add(new Player());

            await context.Players.AddRangeAsync(players);
            await context.SaveChangesAsync();

            var team = new Team();
            await context.Teams.AddAsync(team);

            Assert.Equal(playersCount, context.Players.Count());
            Assert.Equal(0, context.Teams.Count());
            Assert.Throws<DbUpdateConcurrencyException>(() => teamsService.AddNewPlayersAsync(team, players).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(0, "Pesho")]
        [InlineData(1, "Gosho")]
        [InlineData(3, "Stamat")]
        public async Task AddNewPlayersAsync_WithExistingTeam_AddsPlayersToExistingTeam(int numberOfPlayers, string playerName)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();
            var players = new List<Player>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player { Name = playerName });
            }

            var resultTeam = await teamsService.AddNewPlayersAsync(team, players);

            Assert.NotNull(resultTeam);
            Assert.Equal(numberOfPlayers, resultTeam.Players.Count);
            Assert.True(resultTeam.Players.All(p => p.Name == playerName));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task AddNewPlayersAsync_WithExistingTeam_SetsPlayersTeam(int numberOfPlayers)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();

            var players = new List<Player>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player());
            }

            await context.Teams.AddAsync(team);
            await context.Players.AddRangeAsync(players);
            await context.SaveChangesAsync();

            var resultTeam = await teamsService.AddNewPlayersAsync(team, players);

            Assert.NotNull(resultTeam);
            Assert.True(resultTeam.Players.All(p => p.CurrentTeamId == team.Id));
            Assert.True(resultTeam.Players.All(p => p.CurrentTeamId == resultTeam.Id));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task AddNewPlayersAsync_AddsEntitiesToContext(int numberOfPlayers)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();

            var players = new List<Player>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player());
            }

            var resultTeam = await teamsService.AddNewPlayersAsync(team, players);

            Assert.NotNull(resultTeam);
            Assert.Equal(1, context.Teams.Count());
            Assert.Equal(numberOfPlayers, context.Players.Count());
        }

        [Fact]
        public async Task AddManagerAsync_WithNullParameters_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();
            var manager = new Manager();

            var firstResult = await teamsService.AddManagerAsync(null, null);
            var secondResult = await teamsService.AddManagerAsync(team, null);
            var thirdResult = await teamsService.AddManagerAsync(null, manager);

            Assert.Null(firstResult);
            Assert.Null(secondResult);
            Assert.Null(thirdResult);
        }

        [Fact]
        public async Task AddManagerAsync_WithInexistentManager_AddsManagerToContextAndSetsToTeam()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();
            var manager = new Manager();

            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            await context.Managers.AddAsync(manager);

            var resultTeam = await teamsService.AddManagerAsync(team, manager);

            Assert.NotNull(resultTeam);
            Assert.Equal(1, context.Teams.Count());
            Assert.Equal(1, context.Managers.Count());
            Assert.Equal(36, manager.Id.Length);
            Assert.Equal(resultTeam.Manager.Id, manager.Id);
            Assert.Equal(manager.TeamId, resultTeam.Id);
        }

        [Fact]
        public async Task AddManagerAsync_WithInexistentTeamAndManager_AddsEntitiesAndSetsManagerToTeam()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var team = new Team();
            var manager = new Manager();

            var resultTeam = await teamsService.AddManagerAsync(team, manager);

            Assert.NotNull(resultTeam);
            Assert.Equal(1, context.Teams.Count());
            Assert.Equal(1, context.Managers.Count());
            Assert.Equal(36, manager.Id.Length);
            Assert.Equal(resultTeam.Manager.Id, manager.Id);
            Assert.Equal(manager.TeamId, resultTeam.Id);
        }

        [Fact]
        public async Task IncreasePlayersAppearancesAsync_WithNullTeamParam_ReturnsNull()
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var result = await teamsService.IncreasePlayersAppearancesAsync(null);

            Assert.Equal(0, context.Teams.Count());
            Assert.Null(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task IncreasePlayersAppearancesAsync_WithExistingTeamAndInexistentPlayers_AddsPlayersToContextAndIncreasesAppearances(int playersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var teamsService = new TeamsService(context);

            var players = new List<Player>();

            for (int i = 0; i < playersCount; i++)
                players.Add(new Player());

            var team = new Team();

            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            team.Players = players;

            var resultTeam = await teamsService.IncreasePlayersAppearancesAsync(team);

            Assert.NotNull(resultTeam);
            Assert.Equal(1, context.Teams.Count());
            Assert.Equal(playersCount, context.Players.Count());
            Assert.True(players.All(p => p.Id.Length == 36));
            Assert.True(players.All(p => p.Appearances == 1));
            Assert.True(resultTeam.Players.All(p => p.Appearances == 1));
            Assert.True(players.All(p => p.CurrentTeamId == team.Id));
        }
    }
}
