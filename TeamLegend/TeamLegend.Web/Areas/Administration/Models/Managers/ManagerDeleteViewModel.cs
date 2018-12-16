namespace TeamLegend.Web.Areas.Administration.Models.Managers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ManagerDeleteViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name = "Manager Picture")]
        public string ManagerPictureUrl { get; set; }
    }
}
