namespace ServiceModule.Thresholds
{
    public interface IThresholdsDataservice
    {
        void UpdateThresholds(string deviceId, Models.Thresholds thresholds);
    }
}
