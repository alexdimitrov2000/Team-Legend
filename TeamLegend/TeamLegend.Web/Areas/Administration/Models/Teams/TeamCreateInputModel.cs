namespace TeamLegend.Web.Areas.Administration.Models.Teams
{
    using Common;

    using Microsoft.AspNetCore.Http;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TeamCreateInputModel
    {
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

        public IFormFile Badge { get; set; }

        [Display(Name = "Manager")]
        public string ManagerId { get; set; }

        [Display(Name = "Players")]
        public ICollection<string> SquadPlayers { get; set; }
    }
}
