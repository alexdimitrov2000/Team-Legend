namespace TeamLegend.Web.Areas.Administration.Models.Players
{
    using TeamLegend.Models;
    using TeamLegend.Models.Enums;

    using System;

    public class PlayerDeleteViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public int Age { get; set; }

        public double Height { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PlayingPosition PlayingPosition { get; set; }

        public int GoalsScored { get; set; }

        public int Appearances { get; set; }

        public Team CurrentTeam { get; set; }

        public string PlayerPictureUrl { get; set; }
    }
}
