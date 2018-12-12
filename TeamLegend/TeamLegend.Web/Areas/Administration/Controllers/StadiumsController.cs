namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Models.Stadiums;
    using Services.Contracts;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using TeamLegend.Common;

    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class StadiumsController : Controller
    {
        private readonly IStadiumsService stadiumsService;
        private readonly ICloudinaryService cloudinaryService;

        public StadiumsController(IStadiumsService stadiumsService, ICloudinaryService cloudinaryService)
        {
            this.stadiumsService = stadiumsService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StadiumCreateInputModel model)
        {
            var file = model.StadiumPicture;

            var stadiumPictureId = string.Format(GlobalConstants.StadiumPicture, model.Name);
            var fileStream = file.OpenReadStream();

            var imageUploadResult = this.cloudinaryService.UploadStadiumPicture(stadiumPictureId, fileStream);
            await this.stadiumsService.CreateAsync(model.Name, model.Location, model.Capacity, imageUploadResult.Version);

            return this.View();
        }
    }
}
