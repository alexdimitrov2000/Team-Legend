namespace TeamLegend.Models
{
    using Enums;
    using Common;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Name")]
        [StringLength(ValidationConstants.PlayerNameMaxLength, MinimumLength = ValidationConstants.PlayerNameMinLength, ErrorMessage = "Player's name must be at least {2} characters long.")]
        public string Name { get; set; }

        public int Age
        {
            get
            {
                var age = DateTime.UtcNow.Year - this.DateOfBirth.Year;
                if (this.DateOfBirth > DateTime.UtcNow.AddYears(-age) && (age - 1 > 0))
                    age--;

                return age;
            }
        }

        [Required(ErrorMessage = "The Playing Position field is required.")]
        [Display(Name = "Playing Position")]
        [EnumDataType(typeof(PlayingPosition), ErrorMessage = "Please select one of the given positions.")]
        public PlayingPosition PlayingPosition { get; set; }

        [Required(ErrorMessage = "The Date of Birth field is required.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "The Nationality field is required.")]
        [Display(Name = "Nationality")]
        [StringLength(ValidationConstants.PlayerNationalityMaxLength, MinimumLength = ValidationConstants.PlayerNationalityMinLength, ErrorMessage = "Nationality's length must be between {2} and {1} letters long.")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "The Height field is required.")]
        [Display(Name = "Height")]
        public double Height { get; set; }

        public int Appearances { get; set; }

        public int GoalsScored { get; set; }

        public string PlayerPictureVersion { get; set; }

        public string CurrentTeamId { get; set; }
        public virtual Team CurrentTeam { get; set; }
    }
}
