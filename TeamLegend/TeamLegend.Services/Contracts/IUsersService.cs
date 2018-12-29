namespace TeamLegend.Services.Contracts
{
    using Models;

    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<ApplicationUser> SetProfilePictureVersionAsync(ApplicationUser user, string version);
    }
}
