namespace TeamLegend.Web.Models.Managers
{
    using TeamLegend.Models;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class ManagerDetailsViewModel
    {
        private const string ManagerNameDisplay = "Name";
        private const string ManagerAgeDisplay = "Age";
        private const string ManagerDateOfBirthDisplay = "Date of Birth";
        private const string ManagerNationalityDisplay = "Nationality";
        private const string ManagerPictureDisplay = "Manager Picture";
        private const string ManagerTeamDisplay = "Team";

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

        [Display(Name = ManagerTeamDisplay)]
        public Team Team { get; set; }
    }
}
