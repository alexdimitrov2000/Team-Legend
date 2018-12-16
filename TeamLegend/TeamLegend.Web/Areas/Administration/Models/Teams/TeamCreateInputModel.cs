namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using Microsoft.AspNetCore.Http;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class TeamCreateInputModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Team's name length must be at least {2} characters.")]
        public string Name { get; set; }

        [Display(Name = "Nickname")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Team's nickname length must be at least {2} characters.")]
        public string Nickname { get; set; }

        [Display(Name = "Date of Foundation")]
        [DataType(DataType.Date)]
        public DateTime? DateOfFoundation { get; set; }

        public IFormFile Badge { get; set; }
    }
}
