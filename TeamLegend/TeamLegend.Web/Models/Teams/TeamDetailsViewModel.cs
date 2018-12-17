namespace TeamLegend.Web.Models.Teams
{
    using TeamLegend.Models;

    public class TeamDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public string BadgeUrl { get; set; }

        public Manager Manager { get; set; }

        public Stadium Stadium { get; set; }

        public int YearOfFoundation { get; set; }
    }
}
