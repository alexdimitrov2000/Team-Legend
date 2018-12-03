namespace TeamLegend.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common;
    using Contracts;
    using System.IO;

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

        public void Upload(string profilePictureId, Stream fileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(profilePictureId, fileStream),
                Folder = "ProfilePictures",
                PublicId = profilePictureId
            };

            this.cloudinary.Upload(imageUploadParams);
        }

        public string BuildProfilePictureUrl(string username)
        {
            string profilePicture = cloudinary.Api.UrlImgUp.Transform(new Transformation().Gravity("face").Width(30).Height(30).Zoom(0.7).Crop("thumb"))
                                    .BuildUrl("/ProfilePictures/" + string.Format(GlobalConstants.ProfilePicture, username));
            return profilePicture;
        }
    }
}
