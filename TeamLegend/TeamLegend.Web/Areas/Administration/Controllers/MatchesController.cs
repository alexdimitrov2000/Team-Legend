namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models.Matches;
    using Services.Contracts;
    using System.Threading.Tasks;
    using TeamLegend.Models;

    public class MatchesController : AdministrationController
    {
        private readonly IMatchesService matchesService;
        private readonly IMapper mapper;
        private readonly ILogger<MatchesController> logger;
        private readonly IFixturesService fixturesService;

        public MatchesController(IMatchesService matchesService, IMapper mapper, ILogger<MatchesController> logger, IFixturesService fixturesService)
        {
            this.matchesService = matchesService;
            this.mapper = mapper;
            this.logger = logger;
            this.fixturesService = fixturesService;
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
    }
}
