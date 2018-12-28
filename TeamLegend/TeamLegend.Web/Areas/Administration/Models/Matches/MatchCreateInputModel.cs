namespace TeamLegend.Web.Areas.Administration.Models.Matches
{
    using Common;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class MatchCreateInputModel
    {
        private const string HomeTeamDisplay = "Home Team";
        private const string AwayTeamDisplay = "Away Team";
        private const string DateDisplay = "Match Date";
        private const string FixtureRoundDisplay = "Fixture Round";

        private const string DateErrorMessage = "Please provide a valid date.";

        public string LeagueId { get; set; }

        [Required]
        [Display(Name = HomeTeamDisplay)]
        public string HomeTeamId { get; set; }

        [Required]
        [Display(Name = AwayTeamDisplay)]
        public string AwayTeamId { get; set; }

        [Required()]
        [Display(Name = DateDisplay)]
        [DataType(DataType.Date, ErrorMessage = DateErrorMessage)]
        public DateTime? Date { get; set; }

        [Required]
        [Range(ValidationConstants.FixtureRoundMinRange, ValidationConstants.FixtureRoundMaxRange)]
        [Display(Name = FixtureRoundDisplay)]
        public int FixtureRound { get; set; }
    }
}
