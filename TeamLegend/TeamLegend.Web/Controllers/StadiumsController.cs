namespace TeamLegend.Web.Controllers
{
    using Common;
    using Models.Stadiums;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Linq;
    using System.Threading.Tasks;

    public class StadiumsController : BaseController
    {
        private const int NumberOfStadiumsOnPage = GlobalConstants.NumberOfStadiumsOnPage;

        private readonly IStadiumsService stadiumsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;

        public StadiumsController(IStadiumsService stadiumsService, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.stadiumsService = stadiumsService;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Details(string id)
        {
            var stadium = await this.stadiumsService.GetByIdAsync(id);

            if (stadium == null)
            {
                this.ModelState.AddModelError("Error", "Invalid stadium id. Stadium was not found.");
                return this.BadRequest(this.ModelState);
            }

            var stadiumDetailsViewModel = this.mapper.Map<StadiumDetailsViewModel>(stadium);
            stadiumDetailsViewModel.StadiumPictureUrl = this.cloudinaryService.BuildStadiumPictureUrl(stadium.Name, stadium.StadiumPictureVersion);

            return this.View(stadiumDetailsViewModel);
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var stadiums = await this.stadiumsService.GetAllAsync();

            var validatedPage = PageValidator.ValidatePage(page, stadiums.Count(), NumberOfStadiumsOnPage);
            if (validatedPage != page)
                return this.RedirectToAction("All", "Stadiums", new { area = "", page = validatedPage });

            this.ViewData["Page"] = page;
            this.ViewData["HasNextPage"] = ((page + 1) * NumberOfStadiumsOnPage) - NumberOfStadiumsOnPage < stadiums.Count();

            var stadiumModels = stadiums.Select(s => this.mapper.Map<StadiumViewModel>(s))
                                .Skip((page - 1) * NumberOfStadiumsOnPage)
                                .Take(NumberOfStadiumsOnPage)
                                .ToList();

            stadiumModels.ForEach(s => s.StadiumPictureUrl = this.cloudinaryService.BuildStadiumPictureUrl(s.Name, s.StadiumPictureVersion));

            var stadiumCollection = new StadiumsAllCollectionViewModel { Stadiums = stadiumModels };

            return this.View(stadiumCollection);
        }
    }
}
