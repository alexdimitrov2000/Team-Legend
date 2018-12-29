namespace TeamLegend.Services.Tests
{
    using Data;
    using TeamLegend.Models;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class StadiumsServiceTests
    {
        public StadiumsServiceTests()
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

            var stadiumsService = new StadiumsService(context);

            var result = await stadiumsService.CreateAsync(null);

            Assert.Equal(0, context.Stadiums.Count());
            Assert.Null(result);
        }

        [Theory]
        [InlineData("1", "bg")]
        [InlineData("stadiumName", "location")]
        public async Task CreateAsync_WithGivenNameAndLocation_CreatesStadium(string stadiumName, string location)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var stadium = new Stadium { Name = stadiumName, Location = location };

            var resultStadium = await stadiumsService.CreateAsync(stadium);

            Assert.Equal(1, context.Stadiums.Count());
            Assert.Equal(36, resultStadium.Id.Length);
            Assert.Equal(stadiumName, resultStadium.Name);
            Assert.Equal(location, resultStadium.Location);
            Assert.Equal(stadium.Location, resultStadium.Location);
            Assert.Equal(stadium.Name, resultStadium.Name);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("stadiumName")]
        public async Task CreateAsync_WithGivenName_CreatesOnceThenThrows(string stadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var stadium = new Stadium { Name = stadiumName };

            var result = await stadiumsService.CreateAsync(stadium);

            Assert.Equal(1, context.Stadiums.Count());
            Assert.Equal(36, result.Id.Length);

            Assert.Throws<ArgumentException>(() => stadiumsService.CreateAsync(stadium).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task GetByIdAsync_WithInvalidIdParam_ReturnsNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var resultStadium = await stadiumsService.GetByIdAsync(id);

            Assert.Null(resultStadium);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("stadiumId", "stadiumName")]
        public async Task GetByIdAsync_WithExistingStadium_ReturnsExistingStadium(string id, string stadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var existingStadium = new Stadium { Id = id, Name = stadiumName };

            await context.Stadiums.AddAsync(existingStadium);
            await context.SaveChangesAsync();

            var resultStadium = await stadiumsService.GetByIdAsync(id);

            Assert.NotNull(resultStadium);
            Assert.Equal(existingStadium.Id, resultStadium.Id);
            Assert.Equal(existingStadium.Name, resultStadium.Name);
            Assert.Equal(id, resultStadium.Id);
            Assert.Equal(stadiumName, resultStadium.Name);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("stadiumId", "stadiumName")]
        public async Task GetByIdAsync_WithInexistentStadium_ReturnsNull(string id, string stadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var existingStadium = new Stadium { Id = id, Name = stadiumName };

            var resultStadium = await stadiumsService.GetByIdAsync(id);

            Assert.Null(resultStadium);
            Assert.Equal(0, context.Stadiums.Count());
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("stadiumId", "stadiumName")]
        public async Task GetByIdAsync_WithExistingStadiumButInconsistentId_ReturnsNull(string id, string stadiumName)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var existingStadium = new Stadium { Id = id, Name = stadiumName };

            await context.Stadiums.AddAsync(existingStadium);
            await context.SaveChangesAsync();

            var resultStadium = await stadiumsService.GetByIdAsync(id + "null");

            Assert.Null(resultStadium);
            Assert.Equal(1, context.Stadiums.Count());
        }

        [Fact]
        public async Task DeleteAsync_WithNullStadiumParam_ReturnsFalse()
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var isDeleteSuccessful = await stadiumsService.DeleteAsync(null);

            Assert.False(isDeleteSuccessful);
        }

        [Fact]
        public async Task DeleteAsync_WithExistingStadiumParam_ReturnsTrue()
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var stadium = new Stadium();
            await context.Stadiums.AddAsync(stadium);
            await context.SaveChangesAsync();

            var isDeleteSuccessful = await stadiumsService.DeleteAsync(stadium);

            Assert.True(isDeleteSuccessful);
            Assert.Equal(0, context.Stadiums.Count());
        }

        [Theory]
        [InlineData("1")]
        [InlineData("asd")]
        [InlineData("stadiumName")]
        public async Task DeleteAsync_WithInxistentStadiumParam_ThrowsException(string name)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            var stadium = new Stadium { Name = name };

            await context.Stadiums.AddAsync(stadium);
            await context.SaveChangesAsync();

            var stadiumToDelete = new Stadium { Name = name + "diff" };

            Assert.Throws<DbUpdateConcurrencyException>(() => stadiumsService.DeleteAsync(stadiumToDelete).GetAwaiter().GetResult());
            Assert.Equal(1, context.Stadiums.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllAsync_ReturnsAllCreatedStadiums(int stadiumsCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var stadiumsService = new StadiumsService(context);

            for (int i = 0; i < stadiumsCount; i++)
            {
                await context.Stadiums.AddAsync(new Stadium());
            }

            await context.SaveChangesAsync();

            var result = await stadiumsService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(stadiumsCount, result.Count);
        }
    }
}
