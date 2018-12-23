namespace TeamLegend.Web.Controllers
{
    using Models.Players;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    public class PlayersController : BaseController
    {
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
    }
}
