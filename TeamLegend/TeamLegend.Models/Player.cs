namespace TeamLegend.Models
{
    using Enums;
    using Common;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        private const string NameRequiredErrorMessage = "The Name field is required.";
        private const string PlayingPositionRequiredErrorMessage = "The Playing Position field is required.";
        private const string DateOfBirthRequiredErrorMessage = "The Date of Birth field is required.";
        private const string NationalityRequiredErrorMessage = "The Nationality field is required.";
        private const string HeightRequiredErrorMessage = "The Height field is required.";

        private const string NameDisplay = "Name";
        private const string PlayingPositionDisplay = "Playing Position";
        private const string DateOfBirthDisplay = "Date of Birth";
        private const string NationalityDisplay = "Nationality";
        private const string HeightDisplay = "Height";

        private const string NameLengthErrorMessage = "Player's name must be at least {2} characters long.";
        private const string PlayingPositionErrorMessage = "Please select one of the given positions.";
        private const string NationalityLengthErrorMessage = "Nationality's length must be between {2} and {1} letters long.";

        public string Id { get; set; }

        [Required(ErrorMessage = NameRequiredErrorMessage)]
        [Display(Name = NameDisplay)]
        [StringLength(ValidationConstants.PlayerNameMaxLength, MinimumLength = ValidationConstants.PlayerNameMinLength, ErrorMessage = NameLengthErrorMessage)]
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

        [Required(ErrorMessage = PlayingPositionRequiredErrorMessage)]
        [Display(Name = PlayingPositionDisplay)]
        [EnumDataType(typeof(PlayingPosition), ErrorMessage = PlayingPositionErrorMessage)]
        public PlayingPosition PlayingPosition { get; set; }

        [Required(ErrorMessage = DateOfBirthRequiredErrorMessage)]
        [Display(Name = DateOfBirthDisplay)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = NationalityRequiredErrorMessage)]
        [Display(Name = NationalityDisplay)]
        [StringLength(ValidationConstants.PlayerNationalityMaxLength, MinimumLength = ValidationConstants.PlayerNationalityMinLength, ErrorMessage = NationalityLengthErrorMessage)]
        public string Nationality { get; set; }

        [Required(ErrorMessage = HeightRequiredErrorMessage)]
        [Display(Name = HeightDisplay)]
        public double Height { get; set; }

        public int Appearances { get; set; }

        public int GoalsScored { get; set; }

        public string PlayerPictureVersion { get; set; }

        public string CurrentTeamId { get; set; }
        public virtual Team CurrentTeam { get; set; }
    }
}
