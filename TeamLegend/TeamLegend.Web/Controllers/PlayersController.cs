namespace TeamLegend.Web.Controllers
{
    using Common;
    using Models.Players;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class PlayersController : BaseController
    {
        private const int NumberOfPlayersOnPage = GlobalConstants.NumberOfPlayersOnPage;

        private readonly IPlayersService playersService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;

        public PlayersController(IPlayersService playersService, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.playersService = playersService;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Details(string id)
        {
            var player = await this.playersService.GetByIdAsync(id);

            if (player == null)
            {
                this.ModelState.AddModelError("Error", "Invalid player id. Player was not found.");
                return this.BadRequest(this.ModelState);
            }

            var playerDetailsViewModel = this.mapper.Map<PlayerDetailsViewModel>(player);
            playerDetailsViewModel.PlayerPictureUrl = this.cloudinaryService.BuildPlayerPictureUrl(player.Name, player.PlayerPictureVersion);

            return this.View(playerDetailsViewModel);
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var players = await this.playersService.GetAllAsync();

            var validatedPage = PageValidator.ValidatePage(page, players.Count(), NumberOfPlayersOnPage);
            if (validatedPage != page)
                return this.RedirectToAction("All", "Players", new { area = "", page = validatedPage });

            this.ViewData["Page"] = page;
            this.ViewData["HasNextPage"] = ((page + 1) * NumberOfPlayersOnPage) - NumberOfPlayersOnPage < players.Count();

            var playersModels = players
                        .Select(p => this.mapper.Map<PlayerViewModel>(p))
                        .OrderBy(p => p.Name)
                        .Skip((page - 1) * NumberOfPlayersOnPage)
                        .Take(NumberOfPlayersOnPage)
                        .ToList();

            playersModels.ForEach(p => p.PlayerPictureUrl = this.cloudinaryService.BuildPlayerPictureUrl(p.Name, p.PlayerPictureVersion));

            var playersCollection = new PlayersCollectionViewModel
            {
                Players = playersModels
            };

            return this.View(playersCollection);
        }
    }
}
