namespace TeamLegend.Services
{
    using Common;
    using Contracts;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using System.IO;

    public class CloudinaryService : ICloudinaryService
    {
        private const string TeamBadgesFolder = "TeamBadges";
        private const string PlayerPicturesFolder = "PlayerPictures";
        private const string ProfilePicturesFolder = "ProfilePictures";
        private const string StadiumPicturesFolder = "StadiumPictures";

        private readonly Cloudinary cloudinary;

        public CloudinaryService()
        {
            this.cloudinary = new Cloudinary(
                new Account(
                    CloudinaryDataConstants.Cloud,
                    CloudinaryDataConstants.ApiKey,
                    CloudinaryDataConstants.ApiSecret));
        }

        public ImageUploadResult UploadProfilePicture(string profilePictureId, Stream fileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(profilePictureId, fileStream),
                Folder = ProfilePicturesFolder,
                PublicId = profilePictureId
            };

            var result = this.cloudinary.Upload(imageUploadParams);
            return result;
        }

        public ImageUploadResult UploadStadiumPicture(string stadiumPictureId, Stream fileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(stadiumPictureId, fileStream),
                Folder = StadiumPicturesFolder,
                PublicId = stadiumPictureId
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public ImageUploadResult UploadPlayerPicture(string playerPictureId, Stream fileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(playerPictureId, fileStream),
                Folder = PlayerPicturesFolder,
                PublicId = playerPictureId
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public ImageUploadResult UploadTeamBadgePicture(string badgeId, Stream fileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(badgeId, fileStream),
                Folder = TeamBadgesFolder,
                PublicId = badgeId
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public string BuildProfilePictureUrl(string username, string imageVersion)
        {
            if (username == null || imageVersion == null)
                return null;
            
            string path = string.Format(GlobalConstants.FilePath, ProfilePicturesFolder, string.Format(GlobalConstants.ProfilePicture, username));
            var pictureUrl = cloudinary.Api.UrlImgUp.Transform(new Transformation().Gravity("face").Width(30).Height(30).Zoom(0.7).Crop("thumb"))
                                    .Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }

        public string BuildStadiumPictureUrl(string stadiumName, string imageVersion)
        {
            if (stadiumName == null || imageVersion == null)
                return null;
            
            string path = string.Format(GlobalConstants.FilePath, StadiumPicturesFolder, string.Format(GlobalConstants.StadiumPicture, stadiumName));
            return this.BuildPictureUrl(imageVersion, path);
        }

        public string BuildPlayerPictureUrl(string playerName, string imageVersion)
        {
            if (playerName == null || imageVersion == null)
                return null;
            
            string path = string.Format(GlobalConstants.FilePath, PlayerPicturesFolder, string.Format(GlobalConstants.PlayerPicture, playerName));
            return this.BuildPictureUrl(imageVersion, path);
        }

        public string BuildTeamBadgePictureUrl(string teamName, string imageVersion)
        {
            if (teamName == null || imageVersion == null)
                return null;

            string path = string.Format(GlobalConstants.FilePath, TeamBadgesFolder, string.Format(GlobalConstants.BadgePicture, teamName));
            return this.BuildPictureUrl(imageVersion, path);
        }

        private string BuildPictureUrl(string imageVersion, string path)
        {
            var pictureUrl = cloudinary.Api.UrlImgUp.Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }
    }
}
