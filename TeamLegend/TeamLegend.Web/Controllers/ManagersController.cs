namespace TeamLegend.Web.Controllers
{
    using Common;
    using Models.Managers;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class ManagersController : BaseController
    {
        private const int NumberOfManagersOnPage = GlobalConstants.NumberOfManagersOnPage;

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

        public async Task<IActionResult> All(int page = 1)
        {
            var managers = await this.managersService.GetAllAsync();

            var validatedPage = PageValidator.ValidatePage(page, managers.Count(), NumberOfManagersOnPage);
            if (validatedPage != page)
                return this.RedirectToAction("All", "Managers", new { area = "", page = validatedPage });

            this.ViewData["Page"] = page;
            this.ViewData["HasNextPage"] = ((page + 1) * NumberOfManagersOnPage) - NumberOfManagersOnPage < managers.Count();

            var managersModels = managers
                        .Select(p => this.mapper.Map<ManagerViewModel>(p))
                        .OrderBy(p => p.Name)
                        .Skip((page - 1) * NumberOfManagersOnPage)
                        .Take(NumberOfManagersOnPage)
                        .ToList();

            managersModels.ForEach(m => m.ManagerPictureUrl = this.cloudinaryService.BuildManagerPictureUrl(m.Name, m.ManagerPictureVersion));

            var managersCollection = new ManagersCollectionViewModel
            {
                Managers = managersModels
            };

            return this.View(managersCollection);
        }
    }
}
