namespace TeamLegend.Services.Contracts
{
    using CloudinaryDotNet.Actions;

    using System;
    using System.IO;

    public interface ICloudinaryService
    {
        ImageUploadResult UploadPicture(Type entityType, string pictureId, Stream fileStream);

        string BuildProfilePictureUrl(string username, string imageVersion);

        string BuildStadiumPictureUrl(string stadiumName, string imageVersion);

        string BuildPlayerPictureUrl(string playerName, string imageVersion);

        string BuildTeamBadgePictureUrl(string teamName, string imageVersion);

        string BuildManagerPictureUrl(string managerName, string imageVersion);
    }
}
