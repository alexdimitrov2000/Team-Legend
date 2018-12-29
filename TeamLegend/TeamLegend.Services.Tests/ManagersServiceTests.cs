namespace TeamLegend.Services.Tests
{
    using Data;
    using TeamLegend.Models;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ManagersServiceTests
    {
        public ManagersServiceTests()
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

            var managersService = new ManagersService(context);

            var result = await managersService.CreateAsync(null);

            Assert.Equal(0, context.Managers.Count());
            Assert.Null(result);
        }

        [Theory]
        [InlineData("1", "bg")]
        [InlineData("managerName", "nationality")]
        public async Task CreateAsync_WithGivenNameAndNationality_CreatesManager(string managerName, string nationality)
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var manager = new Manager { Name = managerName, Nationality = nationality };

            var resultManager = await managersService.CreateAsync(manager);

            Assert.Equal(1, context.Managers.Count());
            Assert.Equal(36, resultManager.Id.Length);
            Assert.Equal(managerName, resultManager.Name);
            Assert.Equal(nationality, resultManager.Nationality);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetByIdAsync_WithInvalidIdParam_ReturnsNull(string id)
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var resultManager = await managersService.GetByIdAsync(id);

            Assert.Null(resultManager);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("managerId", "managerName")]
        public async Task GetByIdAsync_WithExistingManager_ReturnsExistingManager(string id, string managerName)
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var existingManager = new Manager { Id = id, Name = managerName };

            await context.Managers.AddAsync(existingManager);
            await context.SaveChangesAsync();

            var resultManager = await managersService.GetByIdAsync(id);

            Assert.NotNull(resultManager);
            Assert.Equal(existingManager.Id, resultManager.Id);
            Assert.Equal(existingManager.Name, resultManager.Name);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("managerId", "managerName")]
        public async Task GetByIdAsync_WithInexistentManagerReturnsNull(string id, string managerName)
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var existingManager = new Manager { Id = id, Name = managerName };

            var resultManager = await managersService.GetByIdAsync(id);

            Assert.Null(resultManager);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("abc", "abv")]
        [InlineData("managerId", "managerName")]
        public async Task GetByIdAsync_WithExistingManagerButInconsistentId_ReturnsExistingNull(string id, string managerName)
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var existingManager = new Manager { Id = id, Name = managerName };

            await context.Managers.AddAsync(existingManager);
            await context.SaveChangesAsync();

            var resultManager = await managersService.GetByIdAsync(id + "null");

            Assert.Null(resultManager);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public async Task GetAllAsync_ReturnsAllCreatedManagers(int managersCount)
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            for (int i = 0; i < managersCount; i++)
            {
                await context.Managers.AddAsync(new Manager());
            }

            await context.SaveChangesAsync();

            var result = await managersService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(managersCount, result.Count);
        }

        [Fact]
        public async Task DeleteAsync_WithNullManagerParam_ReturnsFalse()
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var isDeleteSuccessful = await managersService.DeleteAsync(null);

            Assert.False(isDeleteSuccessful);
        }

        [Fact]
        public async Task DeleteAsync_WithExistingManagerParam_ReturnsTrue()
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var manager = new Manager();
            await context.Managers.AddAsync(manager);
            await context.SaveChangesAsync();

            var isDeleteSuccessful = await managersService.DeleteAsync(manager);

            Assert.True(isDeleteSuccessful);
            Assert.Equal(0, context.Managers.Count());
        }

        [Fact]
        public async Task DeleteAsync_WithInxistentManagerParam_ThrowsException()
        {
            var context = new ApplicationDbContext(this.Options);

            var managersService = new ManagersService(context);

            var manager = new Manager { Name = "1" };

            await context.Managers.AddAsync(manager);
            await context.SaveChangesAsync();

            var managerToDelete = new Manager { Name = "2" };

            Assert.Throws<DbUpdateConcurrencyException>(() => managersService.DeleteAsync(managerToDelete).GetAwaiter().GetResult());
            Assert.Equal(1, context.Managers.Count());
        }
    }
}
