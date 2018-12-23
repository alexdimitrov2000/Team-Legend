namespace TeamLegend.Models
{
    using Common;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Matches = new HashSet<Match>();
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(ValidationConstants.TeamNameMaxLength, MinimumLength = ValidationConstants.TeamNameMinLength, ErrorMessage = "Team's name length must be at least {2} characters.")]
        public string Name { get; set; }

        [Display(Name = "Nickname")]
        [StringLength(ValidationConstants.TeamNicknameMaxLength, MinimumLength = ValidationConstants.TeamNicknameMinLength, ErrorMessage = "Team's nickname length must be at least {2} characters.")]
        public string Nickname { get; set; }

        [Display(Name = "Date of Foundation")]
        [DataType(DataType.Date)]
        public DateTime? DateOfFoundation { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsConceded { get; set; }

        public int GoalDifference => this.GoalsScored - this.GoalsConceded;

        public int TotalPoints { get; set; }

        public string BadgeVersion { get; set; }

        public string StadiumId { get; set; }
        public virtual Stadium Stadium { get; set; }

        public string LeagueId { get; set; }
        public virtual League League { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
