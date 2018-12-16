namespace TeamLegend.Web.Areas.Administration.Models.Players
{
    public class PlayerDeleteViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public double Height { get; set; }

        public string DateOfBirth { get; set; }

        public string PlayingPosition { get; set; }

        public int GoalsScored { get; set; }

        public string CurrentTeamName { get; set; }

        public decimal Salary { get; set; }

        public string PlayerPictureUrl { get; set; }
    }
}
