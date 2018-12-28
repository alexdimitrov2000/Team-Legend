namespace TeamLegend.Models
{
    using Common;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        private const string NameDisplay = "Name";
        private const string NicknameDisplay = "Nickname";
        private const string DateOfFoundationDisplay = "Date of Foundation";

        private const string NameLengthErrorMessage = "Team's name length must be at least {2} characters.";
        private const string NicknameLengthErrorMessage = "Team's nickname length must be at least {2} characters.";

        public Team()
        {
            this.Players = new HashSet<Player>();
            this.Matches = new HashSet<Match>();
        }

        public string Id { get; set; }

        [Required]
        [Display(Name = NameDisplay)]
        [StringLength(ValidationConstants.TeamNameMaxLength, MinimumLength = ValidationConstants.TeamNameMinLength, ErrorMessage = NameLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = NicknameDisplay)]
        [StringLength(ValidationConstants.TeamNicknameMaxLength, MinimumLength = ValidationConstants.TeamNicknameMinLength, ErrorMessage = NicknameLengthErrorMessage)]
        public string Nickname { get; set; }

        [Display(Name = DateOfFoundationDisplay)]
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
