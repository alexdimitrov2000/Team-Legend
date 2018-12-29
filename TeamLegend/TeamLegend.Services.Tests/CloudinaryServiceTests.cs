namespace TeamLegend.Services.Tests
{
    using TeamLegend.Services.Contracts;

    using Xunit;

    using System;
    using System.IO;
    using System.Threading.Tasks;
    using TeamLegend.Models;

    public class CloudinaryServiceTests
    {
        private readonly ICloudinaryService cloudinaryService;

        public CloudinaryServiceTests()
        {
            this.cloudinaryService = new CloudinaryService();
        }

        [Theory]
        [InlineData(null, "id", null)]
        [InlineData(typeof(Team), "", null)]
        [InlineData(null, null, null)]
        [InlineData(null, "   ", null)]
        public void UploadPicture_WithNullParameters_ReturnsNull(Type entityType, string pictureId, Stream stream)
        {
            var result = this.cloudinaryService.UploadPicture(entityType, pictureId, stream);
            Assert.Null(result);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("  ", "  ")]
        public void BuildProfilePictureUrl_WithNullParameters_ReturnsNull(string username, string imageVersion)
        {
            var result = this.cloudinaryService.BuildProfilePictureUrl(username, imageVersion);

            Assert.Null(result);
        }

        [Fact]
        public void BuildProfilePictureUrl_ReturnsValidUrl()
        {
            var result = this.cloudinaryService.BuildProfilePictureUrl("username", "version");

            Assert.NotNull(result);
            Assert.EndsWith("_profilePicture", result);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("   ", "  ")]
        public void BuildStadiumPictureUrl_WithNullParameters_ReturnsNull(string stadiumName, string imageVersion)
        {
            var result = this.cloudinaryService.BuildStadiumPictureUrl(stadiumName, imageVersion);

            Assert.Null(result);
        }

        [Fact]
        public void BuildStadiumPictureUrl_ReturnsValidUrl()
        {
            var result = this.cloudinaryService.BuildStadiumPictureUrl("stadium", "version");

            Assert.NotNull(result);
            Assert.EndsWith("_stadiumPicture", result);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("   ", "    ")]
        public void BuildPlayerPictureUrl_WithNullParameters_ReturnsNull(string playerName, string imageVersion)
        {
            var result = this.cloudinaryService.BuildPlayerPictureUrl(playerName, imageVersion);

            Assert.Null(result);
        }

        [Fact]
        public void BuildPlayerPictureUrl_ReturnsValidUrl()
        {
            var result = this.cloudinaryService.BuildPlayerPictureUrl("player", "version");

            Assert.NotNull(result);
            Assert.EndsWith("_playerPicture", result);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("   ", "    ")]
        public void BuildTeamBadgePictureUrl_WithNullParameters_ReturnsNull(string teamName, string imageVersion)
        {
            var result = this.cloudinaryService.BuildTeamBadgePictureUrl(teamName, imageVersion);

            Assert.Null(result);
        }

        [Fact]
        public void BuildTeamBadgePictureUrl_ReturnsValidUrl()
        {
            var result = this.cloudinaryService.BuildTeamBadgePictureUrl("team", "version");

            Assert.NotNull(result);
            Assert.EndsWith("_badge", result);
        }

        [Theory]
        [InlineData("", null)]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("   ", "    ")]
        public void BuildManagerPicturePictureUrl_WithNullParameters_ReturnsNull(string managerName, string imageVersion)
        {
            var result = this.cloudinaryService.BuildManagerPictureUrl(managerName, imageVersion);

            Assert.Null(result);
        }

        [Fact]
        public void BuildManagerPicturePictureUrl_ReturnsValidUrl()
        {
            var result = this.cloudinaryService.BuildManagerPictureUrl("manager", "version");

            Assert.NotNull(result);
            Assert.EndsWith("_managerPicture", result);
        }
    }
}
