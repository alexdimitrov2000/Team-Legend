namespace TeamLegend.Web.Controllers
{
    using Models;
    using Models.Home;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IMatchesService matchesService;
        private readonly IMapper mapper;

        public HomeController(IMatchesService matchesService, IMapper mapper)
        {
            this.matchesService = matchesService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var playedMatches = await this.matchesService.GetAllPlayedAsync();
            var unplayedMatches = await this.matchesService.GetAllUnplayedAsync();

            var lastPlayedMatches = playedMatches.OrderByDescending(m => m.Date)
                .Take(4)
                .Select(m => this.mapper.Map<MatchHomeViewModel>(m))
                .ToList();

            var topComingMatches = unplayedMatches.OrderBy(m => m.Date)
                .Take(4)
                .Select(m => this.mapper.Map<MatchHomeViewModel>(m))
                .ToList();

            var matchCollectionViewModel = new MatchHomeCollectionViewModel
            {
                TopPlayedMatches = lastPlayedMatches,
                TopComingMatches = topComingMatches
            };

            return View(matchCollectionViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
