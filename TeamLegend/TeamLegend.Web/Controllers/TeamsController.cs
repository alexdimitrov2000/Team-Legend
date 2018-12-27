namespace TeamLegend.Web.Controllers
{
    using Common;
    using Models.Teams;
    using Services.Contracts;
    using Areas.Administration.Models.Teams;
    using Areas.Administration.Models.Players;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class TeamsController : BaseController
    {
        private const int NumberOfEntitiesOnPage = GlobalConstants.NumberOfTeamsOnPage;

        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPlayersService playersService;

        public TeamsController(IMapper mapper, ITeamsService teamsService, ICloudinaryService cloudinaryService, IPlayersService playersService)
        {
            this.mapper = mapper;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
            this.playersService = playersService;
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

        public async Task<IActionResult> All(int page = 1)
        {
            var teams = await this.teamsService.GetAllAsync();

            var validatedPage = PageValidator.ValidatePage(page, teams.Count(), NumberOfEntitiesOnPage);
            if (validatedPage != page)
                return this.RedirectToAction("All", "Teams", new { area = "", page = validatedPage });

            var teamsModels = teams.Select(t => this.mapper.Map<TeamViewModel>(t))
                           .Skip((page - 1) * NumberOfEntitiesOnPage)
                           .Take(NumberOfEntitiesOnPage)
                           .ToList();

            this.ViewData["Page"] = page;
            this.ViewData["HasNextPage"] = ((page + 1) * NumberOfEntitiesOnPage) - NumberOfEntitiesOnPage < teams.Count();

            teamsModels.ForEach(t => t.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(t.Name, t.BadgeVersion));

            var teamCollectionViewModel = new TeamsCollectionViewModel { Teams = teamsModels };

            return this.View(teamCollectionViewModel);
        }

        public async Task<IActionResult> SquadList(string teamId)
        {
            var team = await this.teamsService.GetByIdAsync(teamId);

            var squad = this.playersService.GetAllAsync().GetAwaiter().GetResult()
                .Where(p => p.CurrentTeamId == teamId)
                .Select(p => this.mapper.Map<PlayerViewModel>(p))
                .ToList();

            var teamModel = this.mapper.Map<TeamViewModel>(team);
            squad.ForEach(p => p.PlayerPictureUrl = this.cloudinaryService.BuildPlayerPictureUrl(p.Name, p.PlayerPictureVersion));

            var teamPlayersCollection = new TeamPlayersCollectionViewModel
            {
                Team = teamModel,
                Squad = new PlayersCollectionViewModel { Players = squad }
            };

            return this.View(teamPlayersCollection);
        }
    }
}
