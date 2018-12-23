namespace TeamLegend.Services
{
    using Data;
    using Models;
    using Contracts;

    using System.Threading.Tasks;

    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext context;

        public UsersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> SetProfilePictureVersionAsync(ApplicationUser user, string version)
        {
            if (user == null)
                return false;

            user.ProfilePictureVersion = version;
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
