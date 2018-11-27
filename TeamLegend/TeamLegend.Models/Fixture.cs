namespace TeamLegend.Models
{
    using System.Collections.Generic;

    public class Fixture
    {
        public Fixture()
        {
            this.Matches = new HashSet<Match>();
        }

        public string Id { get; set; }

        public int FixtureRound { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}
