namespace TeamLegend.Web.Models.Home
{
    using TeamLegend.Models;

    using System.Collections.Generic;

    public class FixtureMatchesCollectionViewModel
    {
        public string Id { get; set; }

        public int FixtureRound { get; set; }

        public League League { get; set; }

        public List<MatchHomeViewModel> FixtureMatches { get; set; }
    }
}
