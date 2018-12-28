namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using Common;

    using Microsoft.AspNetCore.Http;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TeamCreateInputModel
    {
        private const string NameDisplay = "Name";
        private const string NicknameDisplay = "Nickname";
        private const string FoundationDateDisplay = "Date of Foundation";
        private const string ManagerIdDisplay = "Manager";
        private const string SquadPlayersDisplay = "Players";

        private const string NameLengthErrorMessage = "Team's name length must be at least {2} characters.";
        private const string NicknameLengthErrorMessage = "Team's nickname length must be at least {2} characters.";

        [Required]
        [Display(Name = NameDisplay)]
        [StringLength(ValidationConstants.TeamNameMaxLength, MinimumLength = ValidationConstants.TeamNameMinLength, ErrorMessage = NameLengthErrorMessage)]
        public string Name { get; set; }

        [Display(Name = NicknameDisplay)]
        [StringLength(ValidationConstants.TeamNicknameMaxLength, MinimumLength = ValidationConstants.TeamNicknameMinLength, ErrorMessage = NicknameLengthErrorMessage)]
        public string Nickname { get; set; }

        [Display(Name = FoundationDateDisplay)]
        [DataType(DataType.Date)]
        public DateTime? DateOfFoundation { get; set; }

        public IFormFile Badge { get; set; }

        [Display(Name = ManagerIdDisplay)]
        public string ManagerId { get; set; }

        [Display(Name = SquadPlayersDisplay)]
        public ICollection<string> SquadPlayers { get; set; }
    }
}
