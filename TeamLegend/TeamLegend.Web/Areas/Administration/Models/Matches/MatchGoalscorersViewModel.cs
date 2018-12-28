namespace TeamLegend.Web.Areas.Administration.Models.Matches
{
    using Teams;

    using System.ComponentModel.DataAnnotations;

    public class MatchGoalscorersViewModel
    {
        private const string HomeTeamScorersDisplay = "Home Team Scorers";
        private const string AwayTeamScorersDisplay = "Away Team Scorers";

        public string Id { get; set; }

        public string LeagueId { get; set; }

        public int FixtureRound { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public TeamScorersViewModel HomeTeam { get; set; }

        public TeamScorersViewModel AwayTeam { get; set; }
        
        [Required]
        [Display(Name = HomeTeamScorersDisplay)]
        public string[] HomeTeamScorers { get; set; }

        [Required]
        [Display(Name = AwayTeamScorersDisplay)]
        public string[] AwayTeamScorers { get; set; }
    }
}
