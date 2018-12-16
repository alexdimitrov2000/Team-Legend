namespace TeamLegend.Web.Controllers
{
    using Models.Managers;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    public class ManagersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IManagersService managersService;
        private readonly ICloudinaryService cloudinaryService;

        public ManagersController(IMapper mapper, IManagersService managersService, ICloudinaryService cloudinaryService)
        {
            this.mapper = mapper;
            this.managersService = managersService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var manager = await this.managersService.GetByIdAsync(id);

            if (manager == null)
            {
                this.ModelState.AddModelError("Error", "Invalid manager id. Manager was not found.");
                return this.BadRequest(this.ModelState);
            }

            var managerDetailsViewModel = this.mapper.Map<ManagerDetailsViewModel>(manager);
            managerDetailsViewModel.ManagerPictureUrl = this.cloudinaryService.BuildManagerPictureUrl(manager.Name, manager.ManagerPictureVersion);

            return this.View(managerDetailsViewModel);
        }
    }
}
