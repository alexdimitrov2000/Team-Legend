namespace TeamLegend.Web.Areas.Administration.Models.Matches
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MatchCreateInputModel
    {
        public string LeagueId { get; set; }

        [Required]
        [Display(Name = "Home Team")]
        public string HomeTeamId { get; set; }

        [Required]
        [Display(Name = "Away Team")]
        public string AwayTeamId { get; set; }

        [Required()]
        [Display(Name = "Match Date")]
        [DataType(DataType.Date, ErrorMessage = "Please provide a valid date.")]
        public DateTime? Date { get; set; }

        [Required]
        [Display(Name = "Fixture Round")]
        public int FixtureRound { get; set; }
    }
}
