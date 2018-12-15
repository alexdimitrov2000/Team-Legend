using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using TeamLegend.Models.Enums;

namespace TeamLegend.Web.Areas.Administration.Models.Players
{
    public class PlayerCreateInputModel
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Player's name must be at least {2} characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Date of Birth field is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "The Nationality field is required.")]
        [Display(Name = "Nationality")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nationality's length must be between {2} and {1} letters long.")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "The Height field is required.")]
        [Display(Name = "Height")]
        public double Height { get; set; }

        [Required(ErrorMessage = "The Playing Position field is required.")]
        [Display(Name = "Playing Position")]
        [EnumDataType(typeof(PlayingPosition), ErrorMessage = "Please select one of the given positions.")]
        public PlayingPosition PlayingPosition { get; set; }

        public IFormFile PlayerPicture { get; set; }
    }
}
