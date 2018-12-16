namespace TeamLegend.Web.Controllers
{
    using Models.Stadiums;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    public class StadiumsController : Controller
    {
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
    }
}
