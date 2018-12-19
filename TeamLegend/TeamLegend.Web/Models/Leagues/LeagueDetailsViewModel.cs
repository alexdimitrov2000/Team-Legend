namespace TeamLegend.Web.Models.Leagues
{
    using Teams;

    using System.Collections.Generic;

    public class LeagueDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public List<TeamViewModel> Teams { get; set; }
    }
}
