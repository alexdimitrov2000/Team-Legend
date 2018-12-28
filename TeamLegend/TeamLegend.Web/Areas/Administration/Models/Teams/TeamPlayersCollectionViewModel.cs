namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using Players;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TeamPlayersCollectionViewModel
    {
        private const string ManagerIdDisplay = "Manager";

        public TeamViewModel Team { get; set; }

        public PlayersCollectionViewModel Squad { get; set; }

        public PlayersCollectionViewModel Unemployed { get; set; }

        public List<string> NewPlayers { get; set; }

        [Display(Name = ManagerIdDisplay)]
        public string ManagerId { get; set; }
    }
}
