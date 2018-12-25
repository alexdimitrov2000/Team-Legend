namespace TeamLegend.Web.Models.Home
{
    using System.Collections.Generic;

    public class MatchHomeCollectionViewModel
    {
        public List<MatchHomeViewModel> TopPlayedMatches { get; set; }

        public List<MatchHomeViewModel> TopComingMatches { get; set; }
    }
}
