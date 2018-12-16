namespace TeamLegend.Web.Models.Teams
{
    public class TeamDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string BadgeUrl { get; set; }

        public string ManagerName { get; set; } = "No current manager.";

        public string StadiumName { get; set; } = "No current stadium.";

        public int YearOfFoundation { get; set; }
    }
}
