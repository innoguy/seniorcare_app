using System;

namespace ServiceModule.Thresholds.DataService.Models
{
    public class Thresholds
    {
        public TimeSpan TelevisionFromTime { get; set; }
        public TimeSpan TelevisionToTime { get; set; }

        public int PowerDeviceTime { get; set; }

        public int BathroomGoingTimes { get; set; }
        public TimeSpan BathroomFromTime { get; set; }
        public TimeSpan BathroomToTime { get; set; }

        public TimeSpan PersonNotInBedFrom { get; set; }
        public TimeSpan PersonNotInBedTo { get; set; }

        public Thresholds()
        {
            TelevisionFromTime = TimeSpan.Parse("23:00:00");
            TelevisionToTime = TimeSpan.Parse("06:00:00");

            PowerDeviceTime = 200;

            BathroomGoingTimes = 2;
            BathroomFromTime = TimeSpan.Parse("00:00:00");
            BathroomToTime = TimeSpan.Parse("06:00:00");

            PersonNotInBedFrom = TimeSpan.Parse("00:00:00");
            PersonNotInBedTo = TimeSpan.Parse("06:00:00");
        }
    }
}
