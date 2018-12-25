namespace TeamLegend.Web.Models.Home
{
    using TeamLegend.Models;

    using System;

    public class MatchHomeViewModel
    {
        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public string StadiumName { get; set; }

        public DateTime Date { get; set; }
    }
}
