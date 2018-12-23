namespace TeamLegend.Web.Areas.Administration.Models.Stadiums
{
    using Common;

    using Microsoft.AspNetCore.Http;

    using System.ComponentModel.DataAnnotations;

    public class StadiumCreateInputModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(ValidationConstants.StadiumNameMaxLength, MinimumLength = ValidationConstants.StadiumNameMinLength, ErrorMessage = "Stadium's name must be at least {2} characters long.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Location")]
        [StringLength(ValidationConstants.StadiumLocationMaxLength, MinimumLength = ValidationConstants.StadiumLocationMinLength, ErrorMessage = "Stadium's location must be at least {2} characters long.")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public double Capacity { get; set; }

        [Display(Name = "Stadium Picture")]
        public IFormFile StadiumPicture { get; set; }
    }
}
