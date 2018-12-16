namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using TeamLegend.Models;

    using Microsoft.AspNetCore.Http;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TeamEditViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfFoundation { get; set; }

        public IFormFile Badge { get; set; }

        public string BadgeUrl { get; set; }

        public string BadgeVersion { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsConceded { get; set; }

        public int TotalPoints { get; set; }

        public string StadiumId { get; set; }
        public Stadium Stadium { get; set; }

        public string LeagueId { get; set; }
        public League League { get; set; }

        public Manager Manager { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
