namespace TeamLegend.Web.Areas.Administration.Models.Leagues
{
    using Web.Models.Teams;
    using Web.Models.Leagues;

    using System.Collections.Generic;

    public class LeagueTeamsCollectionViewModel
    {
        public LeagueIndexViewModel League { get; set; }

        public TeamsCollectionViewModel ParticipatingTeams { get; set; }

        public TeamsCollectionViewModel TeamsWithNoLeague { get; set; }

        public List<string> NewTeamsToLeague { get; set; }
    }
}
