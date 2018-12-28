namespace TeamLegend.Models
{
    using Microsoft.AspNetCore.Identity;

    using System;

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CountryOfBirth { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ProfilePictureVersion { get; set; }
    }
}
