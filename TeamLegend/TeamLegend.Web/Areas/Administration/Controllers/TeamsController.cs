namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.Teams;
    using TeamLegend.Common;
    using TeamLegend.Models;
    using Services.Contracts;

    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;

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

                    var imageUploadResult = this.cloudinaryService.UploadTeamBadgePicture(badgeId, fileStream);
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
    }
}
