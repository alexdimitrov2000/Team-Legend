namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using Players;
    using TeamLegend.Web.Models.Teams;

    using System.Collections.Generic;

    public class TeamPlayersCollectionViewModel
    {
        public TeamViewModel Team { get; set; }

        public PlayersCollectionViewModel Squad { get; set; }

        public PlayersCollectionViewModel Unemployed { get; set; }

        public List<string> NewPlayers { get; set; }
    }
}
