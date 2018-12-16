namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using System;

    public class TeamDeleteViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public DateTime? DateOfFoundation { get; set; }

        public string BadgeUrl { get; set; }
        
        public string StadiumName { get; set; }

        public string LeagueName { get; set; }

        public string ManagerName { get; set; }
    }
}
