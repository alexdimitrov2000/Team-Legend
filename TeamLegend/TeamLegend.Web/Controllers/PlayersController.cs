using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamLegend.Services.Contracts;
using TeamLegend.Web.Models.Players;

namespace TeamLegend.Web.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayersService playersService;
        private readonly IMapper mapper;

        public PlayersController(IPlayersService playersService, IMapper mapper)
        {
            this.playersService = playersService;
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

            return this.View(playerDetailsViewModel);
        }
    }
}
