namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models.Stadiums;
    using Services.Contracts;
    using System.Threading.Tasks;
    using TeamLegend.Common;
    using TeamLegend.Models;

    [Area("Administration")]
    [Authorize(Roles = "Admin")]
    public class StadiumsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILogger<StadiumsController> logger;
        private readonly IStadiumsService stadiumsService;
        private readonly ICloudinaryService cloudinaryService;

        public StadiumsController(IMapper mapper, ILogger<StadiumsController> logger, IStadiumsService stadiumsService, ICloudinaryService cloudinaryService)
        {
            this.mapper = mapper;
            this.logger = logger;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var file = model.StadiumPicture;

                var stadium = this.mapper.Map<Stadium>(model);
                if (file != null)
                {
                    var stadiumPictureId = string.Format(GlobalConstants.StadiumPicture, model.Name);
                    var fileStream = file.OpenReadStream();

                    var imageUploadResult = this.cloudinaryService.UploadStadiumPicture(stadiumPictureId, fileStream);
                    stadium.StadiumPictureVersion = imageUploadResult.Version;
                }
                await this.stadiumsService.CreateAsync(stadium);
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
