namespace TeamLegend.Web.Areas.Administration.Models.Matches
{
    using TeamLegend.Models;

    using System.ComponentModel.DataAnnotations;

    public class MatchFinalizeViewModel
    {
        public string Id { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        [Required]
        [Range(0, 10)]
        [Display(Name = "Home Team Goals")]
        public int HomeTeamGoals { get; set; }

        [Required]
        [Range(0, 10)]
        [Display(Name = "Away Team Goals")]
        public int AwayTeamGoals { get; set; }
    }
}
