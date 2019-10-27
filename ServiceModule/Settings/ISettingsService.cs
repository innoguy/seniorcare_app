namespace ServiceModule.Settings
{
    public interface ISettingsService
    {
        string Protocol { get; set; }
        string IpAddress { get; set; }
        string Port { get; set; }
        string TelevisionFromTime { get; set; }
        string TelevisionToTime { get; set; }
        int PowerDeviceTime { get; set; }
        int BathroomGoingTimes { get; set; }
        string BathroomFromTime { get; set; }
        string BathroomToTime { get; set; }
        string PersonNotInBedFrom { get; set; }
        string PersonNotInBedTo { get; set; }
    }
}
