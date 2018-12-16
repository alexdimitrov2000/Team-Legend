namespace TeamLegend.Web.Controllers
{
    using Models.Teams;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    public class TeamsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;

        public TeamsController(IMapper mapper, ITeamsService teamsService, ICloudinaryService cloudinaryService)
        {
            this.mapper = mapper;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var team = await this.teamsService.GetByIdAsync(id);

            if (team == null)
            {
                this.ModelState.AddModelError("Error", "Invalid team id. Team was not found.");
                return this.BadRequest(this.ModelState);
            }

            var teamDetailsViewModel = this.mapper.Map<TeamDetailsViewModel>(team);
            teamDetailsViewModel.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(team.Name, team.BadgeVersion);

            return this.View(teamDetailsViewModel);
        }
    }
}
