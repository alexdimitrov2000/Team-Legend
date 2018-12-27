namespace TeamLegend.Web.ViewComponents
{
    using TeamLegend.Models;
    using Services.Contracts;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;
    using System.Security.Claims;

    [ViewComponent(Name = "UserProfile")]
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public UserProfileViewComponent(UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService)
        {
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal principalUser)
        {
            var user = await this.userManager.GetUserAsync(principalUser);

            var profilePictureUrl = this.cloudinaryService.BuildProfilePictureUrl(user?.UserName, user?.ProfilePictureVersion);

            return this.View((object)profilePictureUrl);
        }
    }
}
