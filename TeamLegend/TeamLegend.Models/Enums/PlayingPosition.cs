namespace TeamLegend.Models.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public enum PlayingPosition
    {
        [Display(Name = "Goalkeeper")] Goalkeeper = 1,
        [Display(Name = "Center Back Defender")] CenterBackDefender = 2,
        [Display(Name = "Left Back Defender")] LeftBackDefender = 3,
        [Display(Name = "Right Back Defender")] RightBackDefender = 4,
        [Display(Name = "Center Midfielder")] CenterMidfielder = 5,
        [Display(Name = "Left Midfielder")] LeftMidfielder = 6,
        [Display(Name = "Right Midfielder")] RightMidfielder = 7,
        [Display(Name = "Center Forward")] CenterForward = 8,
        [Display(Name = "Left Wing Forward")] LeftWingForward = 9,
        [Display(Name = "Right Wing Forward")] RightWingForward = 10,
    }
}
