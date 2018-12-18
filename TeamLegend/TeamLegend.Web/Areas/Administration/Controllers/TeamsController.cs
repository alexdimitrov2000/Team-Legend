namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.Teams;
    using Web.Models.Teams;
    using TeamLegend.Common;
    using TeamLegend.Models;
    using Services.Contracts;
    using Areas.Administration.Models.Players;
    using Areas.Administration.Models.Stadiums;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeamsController : AdministrationController
    {
        private readonly ILogger<TeamsController> logger;
        private readonly IMapper mapper;
        private readonly ITeamsService teamsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IStadiumsService stadiumsService;
        private readonly IPlayersService playersService;

        public TeamsController(ILogger<TeamsController> logger,
                               IMapper mapper, ITeamsService teamsService,
                               ICloudinaryService cloudinaryService,
                               IStadiumsService stadiumsService,
                               IPlayersService playersService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.teamsService = teamsService;
            this.cloudinaryService = cloudinaryService;
            this.stadiumsService = stadiumsService;
            this.playersService = playersService;
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

        public IActionResult SetStadium(string teamId)
        {
            var stadiums = this.stadiumsService.GetAll().Select(s => this.mapper.Map<StadiumViewModel>(s)).ToList();
            stadiums.ForEach(s => s.StadiumPictureUrl = this.cloudinaryService.BuildStadiumPictureUrl(s.Name, s.StadiumPictureVersion));

            var stadiumCollection = new StadiumsCollectionViewModel { Stadiums = stadiums };
            this.ViewData["TeamId"] = teamId;

            return this.View(stadiumCollection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetStadium(string teamId, string stadiumId)
        {
            var team = await this.teamsService.GetByIdAsync(teamId);
            var stadium = await this.stadiumsService.GetByIdAsync(stadiumId);

            try
            {
                await this.teamsService.SetStadiumAsync(team, stadium);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.ViewData["Error"] = $"Could not set stadium {stadium.Name} to {team.Name}.";
            }

            return this.RedirectToAction("Details", "Teams", new { area = "", id = teamId });
        }

        public async Task<IActionResult> SquadList(string teamId)
        {
            var team = await this.teamsService.GetByIdAsync(teamId);

            var squad = this.playersService.GetAllAsync().GetAwaiter().GetResult()
                .Where(p => p.CurrentTeamId == teamId)
                .Select(p => this.mapper.Map<PlayerViewModel>(p))
                .ToList();

            var unemployed = this.playersService.GetAllWithoutTeamAsync().GetAwaiter().GetResult()
                            .Select(p => this.mapper.Map<PlayerViewModel>(p))
                            .ToList();

            var teamModel = this.mapper.Map<TeamViewModel>(team);
            squad.ForEach(p => p.PlayerPictureUrl = this.cloudinaryService.BuildPlayerPictureUrl(p.Name, p.PlayerPictureVersion));
            unemployed.ForEach(p => p.PlayerPictureUrl = this.cloudinaryService.BuildPlayerPictureUrl(p.Name, p.PlayerPictureVersion));

            var teamPlayersCollection = new TeamPlayersCollectionViewModel
            {
                Team = teamModel,
                Squad = new PlayersCollectionViewModel { Players = squad },
                Unemployed = new PlayersCollectionViewModel { Players = unemployed }
            };

            return this.View(teamPlayersCollection);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayers(TeamPlayersCollectionViewModel model)
        {
            var playersIds = model.NewPlayers;
            var teamId = this.TempData["TeamId"].ToString();

            var team = await this.teamsService.GetByIdAsync(teamId);
            var playersToAdd = playersIds.Select(p => this.playersService.GetByIdAsync(p).GetAwaiter().GetResult()).ToList();

            try
            {
                if (playersToAdd.Any(p => p == null))
                    throw new ArgumentException("Not all players exist.");

                await this.teamsService.AddNewPlayersAsync(team, playersToAdd);
            }
            catch (ArgumentException e)
            {
                this.logger.LogError(e.Message);
                this.ViewData["Error"] = e.Message;
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.ViewData["Error"] = $"Could not add all players to {team.Name}. Please try again.";
            }

            return this.RedirectToAction("Details", "Teams", new { area = "", id = team.Id });
        }
    }
}
