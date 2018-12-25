namespace TeamLegend.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Match
    {
        public string Id { get; set; }
        
        public string HomeTeamId { get; set; }
        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; set; }

        public int HomeTeamGoals { get; set; }
        
        public string AwayTeamId { get; set; }
        [ForeignKey(nameof(AwayTeamId))]
        public virtual Team AwayTeam { get; set; }

        public int AwayTeamGoals { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsPlayed { get; set; }
        
        public string FixtureId { get; set; }
        public virtual Fixture Fixture { get; set; }
    }
}
