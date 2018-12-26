namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using Players;

    using System.Collections.Generic;

    public class TeamScorersViewModel
    {
        public string Name { get; set; }

        public string BadgeVersion { get; set; }

        public string BadgeUrl { get; set; }

        public List<PlayerViewModel> Players { get; set; }
    }
}
