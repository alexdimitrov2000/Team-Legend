namespace TeamLegend.Web.Areas.Administration.Models.Managers
{
    using Common;

    using Microsoft.AspNetCore.Http;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class ManagerCreateInputModel
    {
        private const string ManagerNameDisplay = "Name";
        private const string ManagerDateOfBirthDisplay = "Date of Birth";
        private const string ManagerNationalityDisplay = "Nationality";
        private const string ManagerPictureDisplay = "Manager Picture";

        private const string ManagerNameErrorMessage = "Manager's name has to contain at least {2} characters.";
        private const string ManagerNationalityErrorMessage = "Manager's nationality name has to contain at least {2} characters.";

        [Required]
        [Display(Name = ManagerNameDisplay)]
        [StringLength(ValidationConstants.ManagerNameMaxLength, MinimumLength = ValidationConstants.ManagerNameMinLength, ErrorMessage = ManagerNameErrorMessage)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = ManagerDateOfBirthDisplay)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = ManagerNationalityDisplay)]
        [StringLength(ValidationConstants.ManagerNationalityMaxLength, MinimumLength = ValidationConstants.ManagerNationalityMinLength, ErrorMessage = ManagerNationalityErrorMessage)]
        public string Nationality { get; set; }

        [Display(Name = ManagerPictureDisplay)]
        public IFormFile ManagerPicture { get; set; }
    }
}
