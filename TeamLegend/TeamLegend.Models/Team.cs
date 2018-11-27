namespace TeamLegend.Models
{
    using System;
    using System.Collections.Generic;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Matches = new HashSet<Match>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public DateTime DateOfFoundation { get; set; }

        public decimal TeamBudget { get; set; }

        public string StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }

        public string LeagueId { get; set; }
        public virtual League League { get; set; }

        public virtual ApplicationUser Manager { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
