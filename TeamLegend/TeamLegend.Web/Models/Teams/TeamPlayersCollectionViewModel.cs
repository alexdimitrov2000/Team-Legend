namespace TeamLegend.Web.Models.Teams
{
    using Players;

    using System.Collections.Generic;

    public class TeamPlayersCollectionViewModel
    {
        public TeamViewModel Team { get; set; }

        public ICollection<PlayerViewModel> Squad { get; set; }
    }
}
