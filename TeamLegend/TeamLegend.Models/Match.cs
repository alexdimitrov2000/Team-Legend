namespace TeamLegend.Models
{
    using System;

    public class Match
    {
        public string Id { get; set; }

        public string HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public string AwayTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }

        public int AwayTeamGoals { get; set; }

        public DateTime Date { get; set; }

        public string FixtureId { get; set; }
        public virtual Fixture Fixture { get; set; }
    }
}
