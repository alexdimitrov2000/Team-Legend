namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task SetProfilePictureVersionAsync(ApplicationUser user, string version);
    }
}
