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

        public async Task SetProfilePictureVersionAsync(ApplicationUser user, string version)
        {
            user.ProfilePictureVersion = version;
            await this.context.SaveChangesAsync();
        }
    }
}
