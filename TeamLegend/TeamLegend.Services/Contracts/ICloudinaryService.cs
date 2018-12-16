namespace TeamLegend.Services.Contracts
{
    using CloudinaryDotNet.Actions;

    using System.IO;

    public interface ICloudinaryService
    {
        ImageUploadResult UploadProfilePicture(string profilePictureId, Stream fileStream);

        ImageUploadResult UploadStadiumPicture(string stadiumPictureId, Stream fileStream);

        ImageUploadResult UploadPlayerPicture(string playerPictureId, Stream fileStream);

        ImageUploadResult UploadTeamBadgePicture(string badgeId, Stream fileStream);

        string BuildProfilePictureUrl(string username, string imageVersion);

        string BuildStadiumPictureUrl(string stadiumName, string imageVersion);

        string BuildPlayerPictureUrl(string playerName, string imageVersion);

        string BuildTeamBadgePictureUrl(string teamName, string imageVersion);
    }
}
