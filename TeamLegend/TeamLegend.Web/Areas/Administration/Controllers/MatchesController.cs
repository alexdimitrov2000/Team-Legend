namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.Teams;
    using Models.Matches;
    using TeamLegend.Models;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using System.Threading.Tasks;

    public class MatchesController : AdministrationController
    {
        private readonly IMatchesService matchesService;
        private readonly IMapper mapper;
        private readonly ILogger<MatchesController> logger;
        private readonly IFixturesService fixturesService;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPlayersService playersService;

        public MatchesController(IMatchesService matchesService,
                                 IMapper mapper,
                                 ILogger<MatchesController> logger,
                                 IFixturesService fixturesService,
                                 ITeamsService teamsService,
                                 ICloudinaryService cloudinaryService,
                                 IPlayersService playersService)
        {
            this.matchesService = matchesService;
            this.mapper = mapper;
            this.logger = logger;
            this.fixturesService = fixturesService;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
            this.playersService = playersService;
        }

        public IActionResult Create(string leagueId)
        {
            return this.View(new MatchCreateInputModel { LeagueId = leagueId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.HomeTeamId == model.AwayTeamId)
            {
                this.ViewData["Error"] = "Please make sure the two teams are different.";
                return this.View(model);
            }

            var match = this.mapper.Map<Match>(model);

            try
            {
                var fixture = await this.fixturesService.GetOrCreateAsync(model.FixtureRound, model.LeagueId);
                match.Fixture = fixture;

                await this.matchesService.CreateAsync(match);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.ViewData["Error"] = e.InnerException.Message;
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Finalize(string id)
        {
            var match = await this.matchesService.GetByIdAsync(id);

            var matchViewModel = this.mapper.Map<MatchFinalizeViewModel>(match);

            return this.View(matchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalize(MatchFinalizeViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Match match = null;
            try
            {
                match = await this.matchesService.GetByIdAsync(model.Id);
                await this.teamsService.IncreasePlayersAppearancesAsync(match.HomeTeam);
                await this.teamsService.IncreasePlayersAppearancesAsync(match.AwayTeam);

                await this.matchesService.UpdateTeamsGoalsAsync(match, model.HomeTeamGoals, model.AwayTeamGoals);
                await this.matchesService.UpdateScoreAsync(match, model.HomeTeamGoals, model.AwayTeamGoals);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.ViewData["Error"] = e.InnerException.Message;
                return this.View(model);
            }

            return this.RedirectToAction("SetGoalScorers", "Matches", new { matchId = match.Id });
        }

        public async Task<IActionResult> SetGoalscorers(string matchId, string error = null)
        {
            var match = await this.matchesService.GetByIdAsync(matchId);

            var homeTeam = match.HomeTeam;
            var awayTeam = match.AwayTeam;

            var homeTeamViewModel = this.mapper.Map<TeamScorersViewModel>(homeTeam);
            homeTeamViewModel.Players.ToList().ForEach(p => p.PlayerPictureUrl = this.cloudinaryService.BuildPlayerPictureUrl(p.Name, p.PlayerPictureVersion));
            homeTeamViewModel.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(homeTeamViewModel.Name, homeTeamViewModel.BadgeVersion);

            var awayTeamViewModel = this.mapper.Map<TeamScorersViewModel>(awayTeam);
            awayTeamViewModel.Players.ToList().ForEach(p => p.PlayerPictureUrl = this.cloudinaryService.BuildPlayerPictureUrl(p.Name, p.PlayerPictureVersion));
            awayTeamViewModel.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(awayTeamViewModel.Name, awayTeamViewModel.BadgeVersion);

            var matchViewModel = this.mapper.Map<MatchGoalscorersViewModel>(match);
            matchViewModel.HomeTeam = homeTeamViewModel;
            matchViewModel.AwayTeam = awayTeamViewModel;

            matchViewModel.HomeTeamScorers = new string[matchViewModel.HomeTeamGoals];
            matchViewModel.AwayTeamScorers = new string[matchViewModel.AwayTeamGoals];

            this.ViewData["Error"] = error;

            return this.View(matchViewModel);
        }

        [HttpPost, ActionName("SetGoalscorers")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetGoalscorersPost(MatchGoalscorersViewModel model)
        {
            if (!this.ModelState.IsValid && (model.HomeTeamGoals > 0 || model.AwayTeamGoals > 0))
            {
                return this.RedirectToAction("SetGoalscorers", "Matches", new { matchId = model.Id, error = "Please choose a scorer for every goal." });
            }

            var homeTeamPlayers = model.HomeTeamScorers?.Select(p => this.playersService.GetByIdAsync(p).GetAwaiter().GetResult()).ToList();
            var awayTeamPlayers = model.AwayTeamScorers?.Select(p => this.playersService.GetByIdAsync(p).GetAwaiter().GetResult()).ToList();

            try
            {
                await this.playersService.IncreasePlayersGoalsAsync(homeTeamPlayers);
                await this.playersService.IncreasePlayersGoalsAsync(awayTeamPlayers);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                return this.RedirectToAction("SetGoalscorers", "Matches", new { matchId = model.Id, error = e.InnerException.Message });
            }

            return this.RedirectToAction("Details", "Fixtures", new { area = "", leagueId = model.LeagueId, round = model.FixtureRound });
        }
    }
}
