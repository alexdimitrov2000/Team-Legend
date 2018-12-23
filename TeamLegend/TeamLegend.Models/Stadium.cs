namespace TeamLegend.Models
{
    using Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Stadium
    {
        public Stadium()
        {
            this.TenantTeams = new HashSet<Team>();
        }

        public string Id { get; set; }

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

        public string StadiumPictureVersion { get; set; }

        public virtual ICollection<Team> TenantTeams { get; set; }
    }
}
