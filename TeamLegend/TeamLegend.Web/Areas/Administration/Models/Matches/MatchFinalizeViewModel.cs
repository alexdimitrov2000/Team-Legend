namespace TeamLegend.Web.Areas.Administration.Models.Matches
{
    using Common;
    using TeamLegend.Models;

    using System.ComponentModel.DataAnnotations;

    public class MatchFinalizeViewModel
    {
        private const string HomeTeamGoalsDisplay = "Home Team Goals";
        private const string AwayTeamGoalsDisplay = "Away Team Goals";

        public string Id { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        [Required]
        [Range(ValidationConstants.MatchMinimumGoals, ValidationConstants.MatchMaximumGoals)]
        [Display(Name = HomeTeamGoalsDisplay)]
        public int HomeTeamGoals { get; set; }

        [Required]
        [Range(ValidationConstants.MatchMinimumGoals, ValidationConstants.MatchMaximumGoals)]
        [Display(Name = AwayTeamGoalsDisplay)]
        public int AwayTeamGoals { get; set; }
    }
}
