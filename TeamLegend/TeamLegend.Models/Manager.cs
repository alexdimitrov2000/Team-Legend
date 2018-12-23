namespace TeamLegend.Models
{
    using Common;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Manager
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(ValidationConstants.ManagerNameMaxLength, MinimumLength = ValidationConstants.ManagerNameMinLength, ErrorMessage = "Manager's name has to contain at least {2} characters.")]
        public string Name { get; set; }

        public int? Age
        {
            get
            {
                if (this.DateOfBirth == null)
                    return null;

                var age = DateTime.UtcNow.Year - this.DateOfBirth?.Year;
                if (this.DateOfBirth > DateTime.UtcNow.AddYears((int)-age) && (age - 1 > 0))
                    age--;

                return age;
            }
        }
        
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        [StringLength(ValidationConstants.ManagerNationalityMaxLength, MinimumLength = ValidationConstants.ManagerNationalityMinLength, ErrorMessage = "Manager's nationality name has to contain at least {2} characters.")]
        public string Nationality { get; set; }

        public string ManagerPictureVersion { get; set; }

        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
