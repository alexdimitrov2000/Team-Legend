namespace TeamLegend.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CountryOfBirth { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string ProfilePictureId { get; set; }

        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
