namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.League;
    using TeamLegend.Models;
    using Services.Contracts;
    using TeamLegend.Web.Models.Teams;
    using TeamLegend.Web.Models.Leagues;
    using Areas.Administration.Models.Leagues;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class LeaguesController : AdministrationController
    {
        private readonly ILogger<LeaguesController> logger;
        private readonly ILeaguesService leaguesService;
        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;

        public LeaguesController(ILogger<LeaguesController> logger,
                                 ILeaguesService leaguesService,
                                 IMapper mapper,
                                 ITeamsService teamsService,
                                 ICloudinaryService cloudinaryService)
        {
            this.logger = logger;
            this.leaguesService = leaguesService;
            this.mapper = mapper;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            League league;
            try
            {
                league = this.mapper.Map<League>(model);
                await this.leaguesService.CreateAsync(league);

                if (model.ParticipatingTeams != null && model.ParticipatingTeams.Count > 0)
                {
                    var teamIds = model.ParticipatingTeams;
                    var teamsToAdd = teamIds.Select(t => this.teamsService.GetByIdAsync(t).GetAwaiter().GetResult()).ToList();

                    await this.leaguesService.AddTeamsAsync(league, teamsToAdd);
                }
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Details", "Leagues", new { area = "", id = league.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var league = await this.leaguesService.GetByIdAsync(id);

            if (league == null)
                return this.NotFound();

            var leagueDeleteViewModel = this.mapper.Map<LeagueDeleteViewModel>(league);

            return this.View(leagueDeleteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var league = await this.leaguesService.GetByIdAsync(id);

            if (league == null)
                return this.NotFound();

            try
            {
                await this.leaguesService.DeleteAsync(league);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.ModelState.AddModelError("Error", "Could not delete league.");
                return this.BadRequest(this.ModelState);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> TeamsList(string id)
        {
            var league = await this.leaguesService.GetByIdAsync(id);

            var participatingTeams = this.teamsService.GetAllAsync().GetAwaiter().GetResult()
                .Where(t => t.LeagueId == id)
                .Select(t => this.mapper.Map<TeamViewModel>(t))
                .ToList();

            var teamsWithNoLeague = this.teamsService.GetAllWithoutLeagueAsync().GetAwaiter().GetResult()
                            .Select(t => this.mapper.Map<TeamViewModel>(t))
                            .ToList();

            var leagueModel = this.mapper.Map<LeagueIndexViewModel>(league);
            participatingTeams.ForEach(t => t.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(t.Name, t.BadgeVersion));
            teamsWithNoLeague.ForEach(t => t.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(t.Name, t.BadgeVersion));

            var leagueTeamsCollection = new LeagueTeamsCollectionViewModel
            {
                League = leagueModel,
                ParticipatingTeams = new TeamsCollectionViewModel { Teams = participatingTeams },
                TeamsWithNoLeague = new TeamsCollectionViewModel { Teams = teamsWithNoLeague }
            };

            return this.View(leagueTeamsCollection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeams(LeagueTeamsCollectionViewModel model)
        {
            var teamsIds = model.NewTeamsToLeague;
            var leagueId = model.League.Id;

            League league = null;
            try
            {
                league = await this.leaguesService.GetByIdAsync(leagueId);
                var teamsToAdd = teamsIds.Select(t => this.teamsService.GetByIdAsync(t).GetAwaiter().GetResult()).ToList();
                if (teamsToAdd.Any(t => t == null))
                    throw new ArgumentException("Not all teams exist.");

                await this.leaguesService.AddTeamsAsync(league, teamsToAdd);
            }
            catch (ArgumentNullException e)
            {
                this.logger.LogError(e.Message);
                this.TempData["Error"] = "No teams were selected.";
            }
            catch (ArgumentException e)
            {
                this.logger.LogError(e.Message);
                this.TempData["Error"] = e.Message;
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.TempData["Error"] = $"Could not add all teams to {league?.Name}. Please try again.";
            }

            return this.RedirectToAction("Details", "Leagues", new { area = "", id = league.Id });
        }
    }
}
