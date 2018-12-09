using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TeamLegend.Models;
using TeamLegend.Services.Contracts;
using TeamLegend.Web.Areas.Administration.Models.Players;

namespace TeamLegend.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class PlayersController : Controller
    {
        private readonly ILogger<PlayersController> logger;
        private readonly IPlayersService playersService;
        private readonly IMapper mapper;

        public PlayersController(ILogger<PlayersController> logger, IPlayersService playersService, IMapper mapper)
        {
            this.logger = logger;
            this.playersService = playersService;
            this.mapper = mapper;
        }

        public IActionResult Craete()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Craete(PlayerCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var player = this.mapper.Map<Player>(model);
                await this.playersService.CreateAsync(player);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
