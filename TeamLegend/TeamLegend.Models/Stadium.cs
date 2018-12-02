namespace TeamLegend.Models
{
    using System.Collections.Generic;

    public class Stadium
    {
        public Stadium()
        {
            this.TenantTeams = new HashSet<Team>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public double Capacity { get; set; }

        public string StadiumPictureId { get; set; }

        public virtual ICollection<Team> TenantTeams { get; set; }
    }
}
