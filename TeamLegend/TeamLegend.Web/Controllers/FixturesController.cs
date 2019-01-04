namespace TeamLegend.Web.Controllers
{
    using Models.Home;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class FixturesController : BaseController
    {
        private const string FixturesErrorMessage = "There are no fixtures for this league.";

        private readonly IFixturesService fixturesService;
        private readonly IMapper mapper;
        private readonly ILeaguesService leaguesService;

        public FixturesController(IFixturesService fixturesService, IMapper mapper, ILeaguesService leaguesService)
        {
            this.fixturesService = fixturesService;
            this.mapper = mapper;
            this.leaguesService = leaguesService;
        }

        public async Task<IActionResult> Details(string leagueId, int round = 1)
        {
            var validatedRound = await this.fixturesService.ValidateRoundAsync(leagueId, round);

            if (validatedRound == 0)
                return this.RedirectToAction("Details", "Leagues", new { area = "", id = leagueId, fixturesError = FixturesErrorMessage });

            if (round != validatedRound)
                return this.RedirectToAction("Details", "Fixtures", new { area = "", leagueId, round = validatedRound });

            var fixture = await this.fixturesService.GetByLeagueIdAndRoundAsync(leagueId, round);
            var league = await this.leaguesService.GetByIdAsync(leagueId);
            var fixtures = league.Fixtures.Select(f => f.FixtureRound).OrderBy(f => f).ToArray();
            var matches = fixture.Matches.OrderBy(m => m.Date)
                .Select(m => this.mapper.Map<MatchHomeViewModel>(m))
                .ToList();

            var fixtureViewModel = new FixtureMatchesCollectionViewModel
            {
                Id = fixture.Id,
                League = league,
                Fixtures = fixtures,
                FixtureRound = fixture.FixtureRound,
                FixtureMatches = matches
            };

            return this.View(fixtureViewModel);
        }
    }
}
