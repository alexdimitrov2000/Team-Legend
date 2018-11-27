namespace TeamLegend.Models
{
    using System.Collections.Generic;
    
    public class League
    {
        public League()
        {
            this.Teams = new HashSet<Team>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}