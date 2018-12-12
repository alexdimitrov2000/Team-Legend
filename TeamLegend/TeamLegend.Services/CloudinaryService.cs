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

        public ImageUploadResult UploadStadiumPicture(string stadiumName, Stream fileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(stadiumName, fileStream),
                Folder = "StadiumPictures",
                PublicId = stadiumName
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public string BuildProfilePictureUrl(string username, string imageVersion)
        {
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
    }
}
