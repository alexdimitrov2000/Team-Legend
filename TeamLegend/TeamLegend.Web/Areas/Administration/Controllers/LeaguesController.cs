namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.League;
    using TeamLegend.Models;
    using Services.Contracts;
    using Areas.Administration.Models.Leagues;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using System.Threading.Tasks;
    using System.Linq;

    public class LeaguesController : AdministrationController
    {
        private readonly ILogger<LeaguesController> logger;
        private readonly ILeaguesService leaguesService;
        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;

        public LeaguesController(ILogger<LeaguesController> logger, 
                                 ILeaguesService leaguesService, 
                                 IMapper mapper,
                                 ITeamsService teamsService)
        {
            this.logger = logger;
            this.leaguesService = leaguesService;
            this.mapper = mapper;
            this.teamsService = teamsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
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

                if (model.ParticipatingTeams.Count > 0)
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
    }
}
