namespace TeamLegend.Services
{
    using Common;
    using Contracts;
    using TeamLegend.Models;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using System;
    using System.IO;
    using System.Collections.Generic;

    public class CloudinaryService : ICloudinaryService
    {
        private const string TeamBadgesFolder = "TeamBadges";
        private const string PlayerPicturesFolder = "PlayerPictures";
        private const string ProfilePicturesFolder = "ProfilePictures";
        private const string StadiumPicturesFolder = "StadiumPictures";
        private const string ManagerPicturesFolder = "ManagerPictures";

        private readonly Dictionary<Type, string> EntityFolders = new Dictionary<Type, string>
        {
            { typeof(Team), TeamBadgesFolder },
            { typeof(Player), PlayerPicturesFolder },
            { typeof(ApplicationUser), ProfilePicturesFolder },
            { typeof(Stadium), StadiumPicturesFolder },
            { typeof(Manager), ManagerPicturesFolder }
        };
        private readonly Cloudinary cloudinary;

        public CloudinaryService()
        {
            this.cloudinary = new Cloudinary(
                new Account(
                    CloudinaryDataConstants.Cloud,
                    CloudinaryDataConstants.ApiKey,
                    CloudinaryDataConstants.ApiSecret));
        }

        public ImageUploadResult UploadPicture(Type entityType, string pictureId, Stream fileStream)
        {
            if (entityType == null || string.IsNullOrEmpty(pictureId) || string.IsNullOrWhiteSpace(pictureId) || fileStream == null)
                return null;

            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(pictureId, fileStream),
                Folder = this.EntityFolders[entityType],
                PublicId = pictureId
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public string BuildProfilePictureUrl(string username, string imageVersion)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, ProfilePicturesFolder, string.Format(GlobalConstants.ProfilePicture, username));
            var pictureUrl = cloudinary.Api.UrlImgUp.Transform(new Transformation().Gravity("face").Width(30).Height(30).Zoom(0.7).Crop("thumb"))
                                    .Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }

        public string BuildStadiumPictureUrl(string stadiumName, string imageVersion)
        {
            if (string.IsNullOrEmpty(stadiumName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(stadiumName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, StadiumPicturesFolder, string.Format(GlobalConstants.StadiumPicture, stadiumName));
            return this.BuildPictureUrl(imageVersion, path);
        }

        public string BuildPlayerPictureUrl(string playerName, string imageVersion)
        {
            if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(playerName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, PlayerPicturesFolder, string.Format(GlobalConstants.PlayerPicture, playerName));
            return this.BuildPictureUrl(imageVersion, path);
        }

        public string BuildTeamBadgePictureUrl(string teamName, string imageVersion)
        {
            if (string.IsNullOrEmpty(teamName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(teamName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, TeamBadgesFolder, string.Format(GlobalConstants.BadgePicture, teamName));
            return this.BuildPictureUrl(imageVersion, path);
        }

        public string BuildManagerPictureUrl(string managerName, string imageVersion)
        {
            if (string.IsNullOrEmpty(managerName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(managerName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, ManagerPicturesFolder, string.Format(GlobalConstants.ManagerPicture, managerName));
            return this.BuildPictureUrl(imageVersion, path);
        }

        private string BuildPictureUrl(string imageVersion, string path)
        {
            var pictureUrl = cloudinary.Api.UrlImgUp.Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }
    }
}
