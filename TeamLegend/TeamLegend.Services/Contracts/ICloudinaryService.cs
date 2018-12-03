using System.IO;

namespace TeamLegend.Services.Contracts
{
    public interface ICloudinaryService
    {
        void Upload(string profilePictureId, Stream fileStream);

        string BuildProfilePictureUrl(string username);
    }
}
