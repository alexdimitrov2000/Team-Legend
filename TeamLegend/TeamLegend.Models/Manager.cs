namespace TeamLegend.Models
{
    using System;

    public class Manager
    {
        public string Id { get; set; }

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

        public DateTime? DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public string ManagerPictureVersion { get; set; }

        public string TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
