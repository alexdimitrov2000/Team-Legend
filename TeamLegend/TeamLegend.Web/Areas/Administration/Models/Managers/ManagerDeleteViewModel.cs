namespace TeamLegend.Web.Areas.Administration.Models.Managers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ManagerDeleteViewModel
    {
        private const string ManagerNameDisplay = "Name";
        private const string ManagerAgeDisplay = "Age";
        private const string ManagerDateOfBirthDisplay = "Date of Birth";
        private const string ManagerNationalityDisplay = "Nationality";
        private const string ManagerPictureDisplay = "Manager Picture";

        public string Id { get; set; }

        [Display(Name = ManagerNameDisplay)]
        public string Name { get; set; }

        [Display(Name = ManagerAgeDisplay)]
        public int? Age { get; set; }

        [Display(Name = ManagerDateOfBirthDisplay)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = ManagerNationalityDisplay)]
        public string Nationality { get; set; }

        [Display(Name = ManagerPictureDisplay)]
        public string ManagerPictureUrl { get; set; }
    }
}
