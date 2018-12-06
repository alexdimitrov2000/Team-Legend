using System.ComponentModel.DataAnnotations;

namespace TeamLegend.Web.Areas.Administration.Models.League
{
    public class LeagueCreateInputModel
    {
        [Required]
        [Display(Name = "Name*")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "League name length must be in range {2}-{1}.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country*")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Country name length must be in range {2}-{1}.")]
        public string Country { get; set; }
    }
}
