namespace TeamLegend.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common;
    using Contracts;
    using System.IO;
    using Infrastructure.Extensions;

    public class CloudinaryService : ICloudinaryService
    {
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
                Folder = "ProfilePictures",
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
                Folder = "StadiumPictures",
                PublicId = stadiumPictureId
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public ImageUploadResult UploadPlayerPicture(string playerPictureId, Stream fileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(playerPictureId, fileStream),
                Folder = "PlayerPictures",
                PublicId = playerPictureId
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public string BuildProfilePictureUrl(string username, string imageVersion)
        {
            if (username == null || imageVersion == null)
                return null;

            string path = "/ProfilePictures/" + string.Format(GlobalConstants.ProfilePicture, username);
            var pictureUrl = cloudinary.Api.UrlImgUp.Transform(new Transformation().Gravity("face").Width(30).Height(30).Zoom(0.7).Crop("thumb"))
                                    .Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }

        public string BuildStadiumPictureUrl(string stadiumName, string imageVersion)
        {
            string path = "/StadiumPictures/" + string.Format(GlobalConstants.StadiumPicture, stadiumName);
            var pictureUrl = cloudinary.Api.UrlImgUp.Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }

        public string BuildPlayerPictureUrl(string playerName, string imageVersion)
        {
            string path = "/PlayerPictures/" + string.Format(GlobalConstants.PlayerPicture, playerName);
            var pictureUrl = cloudinary.Api.UrlImgUp.Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }
    }
}
