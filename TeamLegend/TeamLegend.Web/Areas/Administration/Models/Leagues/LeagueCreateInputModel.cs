namespace TeamLegend.Web.Areas.Administration.Models.League
{
    using Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LeagueCreateInputModel
    {
        private const string LeagueNameDisplay = "Name";
        private const string LeagueCountryDisplay = "Country";
        private const string LeagueParticipatingTeamsDisplay = "Participating Teams";

        private const string LeagueNameErrorMessage = "League name length must be in range {2}-{1}.";
        private const string LeagueCountryErrorMessage = "Country name length must be in range {2}-{1}.";

        [Required]
        [Display(Name = LeagueNameDisplay)]
        [StringLength(ValidationConstants.LeagueNameMaxLength, MinimumLength = ValidationConstants.LeagueNameMinLength, ErrorMessage = LeagueNameErrorMessage)]
        public string Name { get; set; }

        [Required]
        [Display(Name = LeagueCountryDisplay)]
        [StringLength(ValidationConstants.LeagueCountryMaxLength, MinimumLength = ValidationConstants.LeagueCountryMinLength, ErrorMessage = LeagueCountryErrorMessage)]
        public string Country { get; set; }

        [Display(Name = LeagueParticipatingTeamsDisplay)]
        public List<string> ParticipatingTeams { get; set; }
    }
}
