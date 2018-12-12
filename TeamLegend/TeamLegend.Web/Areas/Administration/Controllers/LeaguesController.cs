namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.League;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Castle.Core.Logging;
    using TeamLegend.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using System;
    using Microsoft.Extensions.Logging;

    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class LeaguesController : Controller
    {
        private readonly ILogger<LeaguesController> logger;
        private readonly ILeaguesService leaguesService;

        public LeaguesController(ILogger<LeaguesController> logger, ILeaguesService leaguesService)
        {
            this.logger = logger;
            this.leaguesService = leaguesService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(LeagueCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                this.leaguesService.CreateAsync(model.Name, model.Country);
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
