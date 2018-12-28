namespace TeamLegend.Models
{
    using Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class League
    {
        private const string NameDisplay = "Name";
        private const string CountryDisplay = "Country";

        private const string NameLengthErrorMessage = "League name length must be in range {2}-{1}.";
        private const string CountryLengthErrorMessage = "Country name length must be in range {2}-{1}.";

        public League()
        {
            this.Teams = new HashSet<Team>();
            this.Fixtures = new HashSet<Fixture>();
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = NameDisplay)]
        [StringLength(ValidationConstants.LeagueNameMaxLength, MinimumLength = ValidationConstants.LeagueNameMinLength, ErrorMessage = NameLengthErrorMessage)]
        public string Name { get; set; }

        [Required]
        [Display(Name = CountryDisplay)]
        [StringLength(ValidationConstants.LeagueCountryMaxLength, MinimumLength = ValidationConstants.LeagueCountryMinLength, ErrorMessage = CountryLengthErrorMessage)]
        public string Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Fixture> Fixtures { get; set; }
    }
}