namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    public class TeamViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string BadgeVersion { get; set; }

        public string BadgeUrl { get; set; }

        public int GoalDifference { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsConceded { get; set; }

        public int TotalPoints { get; set; }

        public int MatchesPlayed { get; set; }
    }
}
