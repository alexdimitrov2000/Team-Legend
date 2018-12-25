namespace TeamLegend.Web.Controllers
{
    using Models.Leagues;
    using Services.Contracts;
    using Web.Models.Teams;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class LeaguesController : BaseController
    {
        private readonly ILeaguesService leagueService;
        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMatchesService matchesService;

        public LeaguesController(ILeaguesService leagueService, IMapper mapper, ITeamsService teamsService, ICloudinaryService cloudinaryService, IMatchesService matchesService)
        {
            this.leagueService = leagueService;
            this.mapper = mapper;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
            this.matchesService = matchesService;
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

            var matches = await this.matchesService.GetAllPlayedAsync();

            var participatingTeams = this.teamsService.GetAllAsync().GetAwaiter().GetResult()
                .Where(t => t.LeagueId == id)
                .Select(t => this.mapper.Map<TeamViewModel>(t))
                .OrderByDescending(t => t.TotalPoints)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.GoalsScored)
                .ToList();

            participatingTeams.ForEach(t => t.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(t.Name, t.BadgeVersion));
            participatingTeams.ForEach(t => t.MatchesPlayed = matches.Where(m => m.HomeTeamId == t.Id || m.AwayTeamId == t.Id).ToArray().Length);

            var leagueDetailsViewModel = this.mapper.Map<LeagueDetailsViewModel>(league);
            leagueDetailsViewModel.Teams = participatingTeams;

            return this.View(leagueDetailsViewModel);
        }
    }
}
