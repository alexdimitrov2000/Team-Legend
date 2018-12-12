using CloudinaryDotNet.Actions;
using System.IO;

namespace TeamLegend.Services.Contracts
{
    public interface ICloudinaryService
    {
        ImageUploadResult UploadProfilePicture(string profilePictureId, Stream fileStream);

        ImageUploadResult UploadStadiumPicture(string stadiumPictureId, Stream fileStream);

        ImageUploadResult UploadPlayerPicture(string playerPictureId, Stream fileStream);

        string BuildProfilePictureUrl(string username, string imageVersion);

        string BuildStadiumPictureUrl(string stadiumName, string imageVersion);

        string BuildPlayerPictureUrl(string playerName, string imageVersion);
    }
}
