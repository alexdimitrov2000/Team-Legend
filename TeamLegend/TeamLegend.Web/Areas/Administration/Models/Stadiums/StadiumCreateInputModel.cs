namespace TeamLegend.Web.Areas.Administration.Models.Stadiums
{
    using Common;

    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    public class StadiumCreateInputModel
    {
        private const string NameDisplay = "Name";
        private const string LocationDisplay = "Location";
        private const string CapacityDisplay = "Capacity";
        private const string StadiumPictureDisplay = "Stadium Picture";

        private const string NameLengthErrorMessage = "Stadium's name must be at least {2} characters long.";
        private const string LocationLengthErrorMessage = "Stadium's location must be at least {2} characters long.";

        [Required]
        [Display(Name = NameDisplay)]
        [StringLength(ValidationConstants.StadiumNameMaxLength, MinimumLength = ValidationConstants.StadiumNameMinLength, ErrorMessage = NameLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        [Display(Name = LocationDisplay)]
        [StringLength(ValidationConstants.StadiumLocationMaxLength, MinimumLength = ValidationConstants.StadiumLocationMinLength, ErrorMessage = LocationLengthErrorMessage)]
        public string Location { get; set; }

        [Required]
        [Display(Name = CapacityDisplay)]
        public double Capacity { get; set; }

        [Display(Name = StadiumPictureDisplay)]
        public IFormFile StadiumPicture { get; set; }
    }
}
