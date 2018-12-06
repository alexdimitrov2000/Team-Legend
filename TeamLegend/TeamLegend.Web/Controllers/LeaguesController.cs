namespace TeamLegend.Web.Controllers
{
    using Models.Leagues;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class LeaguesController : Controller
    {
        private readonly ILeaguesService leagueService;
        private readonly IMapper mapper;

        public LeaguesController(ILeaguesService leagueService, IMapper mapper)
        {
            this.leagueService = leagueService;
            this.mapper = mapper;
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
            var leagueDetailsViewModel = this.mapper.Map<LeagueDetailsViewModel>(league);

            return this.View(leagueDetailsViewModel);
        }
    }
}
