namespace TeamLegend.Models
{
    using Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Stadium
    {
        private const string NameDisplay = "Name";
        private const string LocationDisplay = "Location";
        private const string CapacityDisplay = "Capacity";

        private const string NameLengthErrorMessage = "Stadium's name must be at least {2} characters long.";
        private const string LocationLengthErrorMessage = "Stadium's location must be at least {2} characters long.";

        public Stadium()
        {
            this.TenantTeams = new HashSet<Team>();
        }

        public string Id { get; set; }

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

        public string StadiumPictureVersion { get; set; }

        public virtual ICollection<Team> TenantTeams { get; set; }
    }
}
