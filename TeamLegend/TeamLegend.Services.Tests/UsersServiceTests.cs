namespace TeamLegend.Services.Tests
{
    using Data;
    using TeamLegend.Models;

    using Xunit;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersServiceTests
    {
        public UsersServiceTests()
        {
            this.Options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
        }

        public DbContextOptions<ApplicationDbContext> Options { get; }

        [Theory]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData(null, "   ")]
        public async Task SetProfilePictureVersionAsync_WithInvalidParams_ReturnsNull(ApplicationUser user, string version)
        {
            var context = new ApplicationDbContext(this.Options);

            var usersService = new UsersService(context);

            var result = await usersService.SetProfilePictureVersionAsync(user, version);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("a")]
        [InlineData("pictureVersion")]
        public async Task SetProfilePictureVersionAsync_WithValidParams_ReturnsUserWithSetVersion(string version)
        {
            var context = new ApplicationDbContext(this.Options);

            var usersService = new UsersService(context);

            var user = new ApplicationUser();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var resultUser = await usersService.SetProfilePictureVersionAsync(user, version);

            Assert.NotNull(resultUser);
            Assert.Equal(1, context.Users.Count());
            Assert.Equal(version, resultUser.ProfilePictureVersion);
            Assert.Equal(user.ProfilePictureVersion, resultUser.ProfilePictureVersion);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("a")]
        [InlineData("pictureVersion")]
        public void SetProfilePictureVersionAsync_WithInexistentUser_ThrowsException(string version)
        {
            var context = new ApplicationDbContext(this.Options);

            var usersService = new UsersService(context);

            var user = new ApplicationUser();

            Assert.Throws<DbUpdateConcurrencyException>(() => usersService.SetProfilePictureVersionAsync(user, version).GetAwaiter().GetResult());
            Assert.Equal(0, context.Users.Count());
        }
    }
}
