namespace TeamLegend.Models
{
    using Common;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Manager
    {
        private const string NameDisplay = "Name";
        private const string DateOfBirthDisplay = "Date of Birth";
        private const string NationalityDisplay = "Nationality";

        private const string NameLengthErrorMessage = "Manager's name has to contain at least {2} characters.";
        private const string NationalityLengthErrorMessage = "Manager's nationality name has to contain at least {2} characters.";

        public string Id { get; set; }

        [Required]
        [Display(Name = NameDisplay)]
        [StringLength(ValidationConstants.ManagerNameMaxLength, MinimumLength = ValidationConstants.ManagerNameMinLength, ErrorMessage = NameLengthErrorMessage)]
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
        [Display(Name = DateOfBirthDisplay)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = NationalityDisplay)]
        [StringLength(ValidationConstants.ManagerNationalityMaxLength, MinimumLength = ValidationConstants.ManagerNationalityMinLength, ErrorMessage = NationalityLengthErrorMessage)]
        public string Nationality { get; set; }

        public string ManagerPictureVersion { get; set; }

        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
