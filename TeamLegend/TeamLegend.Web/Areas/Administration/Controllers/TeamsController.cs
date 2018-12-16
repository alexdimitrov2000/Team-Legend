namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models.Teams;
    using Services.Contracts;
    using System.Threading.Tasks;
    using TeamLegend.Common;
    using TeamLegend.Models;

    public class TeamsController : AdministrationController
    {
        private readonly ILogger<TeamsController> logger;
        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;

        public TeamsController(ILogger<TeamsController> logger, IMapper mapper, ITeamsService teamsService, ICloudinaryService cloudinaryService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Team team;
            try
            {
                team = this.mapper.Map<Team>(model);

                var file = model.Badge;
                if (file != null)
                {
                    var badgeId = string.Format(GlobalConstants.BadgePicture, model.Name);
                    var fileStream = file.OpenReadStream();

                    var imageUploadResult = this.cloudinaryService.UploadPicture(team.GetType(), badgeId, fileStream);
                    team.BadgeVersion = imageUploadResult.Version;
                }
                await this.teamsService.CreateAsync(team);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Details", "Teams", new { area = "", id = team.Id });
        }

        // ---- TO BE FINISHED
        //public async Task<IActionResult> Edit(string id)
        //{
        //    var team = await this.teamsService.GetByIdAsync(id);

        //    if (team == null)
        //        return this.NotFound();

        //    var teamEditViewModel = this.mapper.Map<TeamEditViewModel>(team);
        //    teamEditViewModel.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(team.Name, team.BadgeVersion);

        //    return this.View(teamEditViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(TeamEditViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //    Team team = new Team();
        //    try
        //    {
        //        var file = model.Badge;
        //        if (file != null)
        //        {
        //            var badgeId = string.Format(GlobalConstants.BadgePicture, model.Name);
        //            var fileStream = file.OpenReadStream();

        //            var imageUploadResult = this.cloudinaryService.UploadPicture(team.GetType(), badgeId, fileStream);
        //            model.BadgeVersion = imageUploadResult.Version;
        //        }
        //        team = this.mapper.Map<Team>(model);

        //        await this.teamsService.UpdateAsync(team);
        //    }
        //    catch (DbUpdateException e)
        //    {
        //        this.logger.LogError(e.Message);
        //        return this.View(model);
        //    }

        //    return this.RedirectToAction("Details", "Teams", new { area = "", id = team.Id });
        //}
        
        public async Task<IActionResult> Delete(string id)
        {
            var team = await this.teamsService.GetByIdAsync(id);

            if (team == null)
                return this.NotFound();

            var teamDeleteViewModel = this.mapper.Map<TeamDeleteViewModel>(team);
            teamDeleteViewModel.BadgeUrl = this.cloudinaryService.BuildTeamBadgePictureUrl(team.Name, team.BadgeVersion);

            return this.View(teamDeleteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var team = await this.teamsService.GetByIdAsync(id);

            if (team == null)
                return this.NotFound();

            try
            {
                await this.teamsService.DeleteAsync(team);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.ModelState.AddModelError("Error", "Could not delete team.");
                return this.BadRequest(this.ModelState);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
