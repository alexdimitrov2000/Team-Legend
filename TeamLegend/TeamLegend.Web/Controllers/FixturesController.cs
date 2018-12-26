namespace TeamLegend.Web.Controllers
{
    using Models.Home;
    using Services.Contracts;

    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;

    public class FixturesController : BaseController
    {
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
            var fixture = await this.fixturesService.GetByLeagueIdAndRoundAsync(leagueId, round);
            var league = await this.leaguesService.GetByIdAsync(leagueId);
            var matches = fixture.Matches.OrderBy(m => m.Date)
                .Select(m => this.mapper.Map<MatchHomeViewModel>(m))
                .ToList();

            var fixtureViewModel = new FixtureMatchesCollectionViewModel
            {
                Id = fixture.Id,
                League = league,
                FixtureRound = fixture.FixtureRound,
                FixtureMatches = matches
            };

            return this.View(fixtureViewModel);
        }
    }
}
