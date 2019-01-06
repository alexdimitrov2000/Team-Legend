namespace TeamLegend.Models
{
    using TeamLegend.Common;

    using Microsoft.AspNetCore.Identity;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        [StringLength(ValidationConstants.UserFirstNameMaxLength, MinimumLength = ValidationConstants.UserFirstNameMinLength)]
        public string FirstName { get; set; }

        [StringLength(ValidationConstants.UserLastNameMaxLength, MinimumLength = ValidationConstants.UserLastNameMinLength)]
        public string LastName { get; set; }

        public string CountryOfBirth { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ProfilePictureVersion { get; set; }
    }
}
