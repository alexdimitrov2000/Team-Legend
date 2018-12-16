namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Common;
    using TeamLegend.Models;
    using Services.Contracts;
    using Areas.Administration.Models.Managers;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using System.Threading.Tasks;

    public class ManagersController : AdministrationController
    {
        private readonly ILogger<ManagersController> logger;
        private readonly IMapper mapper;
        private readonly IManagersService managersService;
        private readonly ICloudinaryService cloudinaryService;

        public ManagersController(ILogger<ManagersController> logger, IMapper mapper, IManagersService managersService, ICloudinaryService cloudinaryService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.managersService = managersService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagerCreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            Manager manager;
            try
            {
                manager = this.mapper.Map<Manager>(model);

                var file = model.ManagerPicture;
                if (file != null)
                {
                    var managerPictureId = string.Format(GlobalConstants.ManagerPicture, model.Name);
                    var fileStream = file.OpenReadStream();

                    var imageUploadResult = this.cloudinaryService.UploadPicture(manager.GetType(), managerPictureId, fileStream);
                    manager.ManagerPictureVersion = imageUploadResult.Version;
                }
                await this.managersService.CreateAsync(manager);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                return this.View(model);
            }

            return this.RedirectToAction("Details", "Managers", new { area = "", id = manager.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var manager = await this.managersService.GetByIdAsync(id);

            if (manager == null)
                return this.NotFound();

            var managerDeleteViewModel = this.mapper.Map<ManagerDeleteViewModel>(manager);
            managerDeleteViewModel.ManagerPictureUrl = this.cloudinaryService.BuildManagerPictureUrl(manager.Name, manager.ManagerPictureVersion);

            return this.View(managerDeleteViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var league = await this.managersService.GetByIdAsync(id);

            if (league == null)
                return this.NotFound();

            try
            {
                await this.managersService.DeleteAsync(league);
            }
            catch (DbUpdateException e)
            {
                this.logger.LogError(e.Message);
                this.ModelState.AddModelError("Error", "Could not delete manager.");
                return this.BadRequest(this.ModelState);
            }

            return this.RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
