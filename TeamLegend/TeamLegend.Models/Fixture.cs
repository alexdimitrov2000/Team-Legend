namespace TeamLegend.Models
{
    using Common;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Fixture
    {
        public Fixture()
        {
            this.Matches = new HashSet<Match>();
        }

        public string Id { get; set; }

        [Required]
        [Range(ValidationConstants.FixtureRoundMinRange, ValidationConstants.FixtureRoundMaxRange)]
        public int FixtureRound { get; set; }
        
        public string LeagueId { get; set; }
        public virtual League League { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
