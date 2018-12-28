namespace TeamLegend.Web.Areas.Administration.Models.Players
{
    using Common;
    using TeamLegend.Models.Enums;

    using Microsoft.AspNetCore.Http;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class PlayerCreateInputModel
    {
        private const string NameRequiredErrorMessage = "The Name field is required.";
        private const string DateOfBirthRequiredErrorMessage = "The Date of Birth field is required.";
        private const string NationalityRequiredErrorMessage = "The Nationality field is required.";
        private const string HeightRequiredErrorMessage = "The Height field is required.";
        private const string PlayingPositionRequiredErrorMessage = "The Playing Position field is required.";

        private const string NameDisplay = "Name";
        private const string DateOfBirthDisplay = "Date of Birth";
        private const string NationalityDisplay = "Nationality";
        private const string HeightDisplay = "Height";
        private const string PlayingPositionDisplay = "Playing Position";
        private const string PlayerPictureDisplay = "Player Picture";

        private const string NameLengthErrorMessage = "Player's name must be at least {2} characters long.";
        private const string NationalityLengthErrorMessage = "Nationality's length must be between {2} and {1} letters long.";
        private const string PlayingPositionEnumErrorMessage = "Please select one of the given positions.";

        [Required(ErrorMessage = NameRequiredErrorMessage)]
        [Display(Name = NameDisplay)]
        [StringLength(ValidationConstants.PlayerNameMaxLength, MinimumLength = ValidationConstants.PlayerNameMinLength, ErrorMessage = NameLengthErrorMessage)]
        public string Name { get; set; }

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

        [Required(ErrorMessage = PlayingPositionRequiredErrorMessage)]
        [Display(Name = PlayingPositionDisplay)]
        [EnumDataType(typeof(PlayingPosition), ErrorMessage = PlayingPositionEnumErrorMessage)]
        public PlayingPosition PlayingPosition { get; set; }

        public string TeamId { get; set; }

        [Display(Name = PlayerPictureDisplay)]
        public IFormFile PlayerPicture { get; set; }
    }
}
