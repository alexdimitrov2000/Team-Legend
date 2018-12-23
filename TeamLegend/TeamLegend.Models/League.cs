namespace TeamLegend.Models
{
    using Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class League
    {
        public League()
        {
            this.Teams = new HashSet<Team>();
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(ValidationConstants.LeagueNameMaxLength, MinimumLength = ValidationConstants.LeagueNameMinLength, ErrorMessage = "League name length must be in range {2}-{1}.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country")]
        [StringLength(ValidationConstants.LeagueCountryMaxLength, MinimumLength = ValidationConstants.LeagueCountryMinLength, ErrorMessage = "Country name length must be in range {2}-{1}.")]
        public string Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}