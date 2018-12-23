namespace TeamLegend.Web.Controllers
{
    using Models.Leagues;
    using Services.Contracts;
    using Web.Models.Teams;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class LeaguesController : Controller
    {
        private readonly ILeaguesService leagueService;
        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;

        public LeaguesController(ILeaguesService leagueService, IMapper mapper, ITeamsService teamsService, ICloudinaryService cloudinaryService)
        {
            this.leagueService = leagueService;
            this.mapper = mapper;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> All()
        {
            var leagues = (await this.leagueService.GetAllAsync())
                .Select(l => this.mapper.Map<LeagueIndexViewModel>(l))
                .ToList();

            var leagueAllViewModel = new LeagueAllViewModel { Leagues = leagues };
            return this.View(leagueAllViewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var league = await this.leagueService.GetByIdAsync(id);
            if (league == null)
            {
                this.ModelState.AddModelError("Error", "Invalid league id. League was not found.");
                return this.BadRequest(this.ModelState);
            }

            if (this.TempData["Error"] != null)
                this.TempData.Keep("Error");

            var participatingTeams = this.teamsService.GetAllAsync().GetAwaiter().GetResult()
                .Where(t => t.LeagueId == id)
                .Select(t => this.mapper.Map<TeamViewModel>(t))
                .ToList();
            participatingTeams.ForEach(t => t.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(t.Name, t.BadgeVersion));

            var leagueDetailsViewModel = this.mapper.Map<LeagueDetailsViewModel>(league);
            leagueDetailsViewModel.Teams = participatingTeams;

            return this.View(leagueDetailsViewModel);
        }
    }
}
