namespace TeamLegend.Models.Enums
{
    using System.ComponentModel;
    
    public enum PlayingPosition
    {
        [DisplayName("Goalkeeper")] Goalkeeper = 1,
        [DisplayName("Center Back Defender")] CenterBackDefender = 2,
        [DisplayName("Left Back Defender")] LeftBackDefender = 3,
        [DisplayName("Right Back Defender")] RightBackDefender = 4,
        [DisplayName("Center Midfielder")] CenterMidfielder = 5,
        [DisplayName("Left Midfielder")] LeftMidfielder = 6,
        [DisplayName("Right Midfielder")] RightMidfielder = 7,
        [DisplayName("Center Forward")] CenterForward = 8,
        [DisplayName("Left Wing Forward")] LeftWingForward = 9,
        [DisplayName("Right Wing Forward")] RightWingForward = 10,
    }
}
