using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TeamLegend.Web.Areas.Administration.Models.Stadiums
{
    public class StadiumCreateInputModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Stadium's name must be at least {2} characters long.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Location")]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "Stadium's location must be at least {2} characters long.")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public double Capacity { get; set; }

        [Display(Name = "Stadium Picture")]
        public IFormFile StadiumPicture { get; set; }
    }
}
