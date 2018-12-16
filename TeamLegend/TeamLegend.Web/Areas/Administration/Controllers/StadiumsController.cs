namespace TeamLegend.Web.Areas.Administration.Controllers
{
    using Common;
    using Models.Stadiums;
    using TeamLegend.Models;
    using Services.Contracts;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using System.Threading.Tasks;

    public class StadiumsController : AdministrationController
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

            Stadium stadium;
            try
            {
                var file = model.StadiumPicture;

                stadium = this.mapper.Map<Stadium>(model);
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

            return this.RedirectToAction("Details", "Stadiums", new { area = "", id = stadium.Id });
        }
    }
}
