using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace ServiceModule.Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;

        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }

        public string TelevisionFromTime
        {
            get => _settings.GetValueOrDefault(nameof(TelevisionFromTime), "23:00:00");
            set => _settings.AddOrUpdateValue(nameof(TelevisionFromTime), value);
        }

        public string TelevisionToTime
        {
            get => _settings.GetValueOrDefault(nameof(TelevisionToTime), "06:00:00");
            set => _settings.AddOrUpdateValue(nameof(TelevisionToTime), value);
        }

        public int PowerDeviceTime
        {
            get => _settings.GetValueOrDefault(nameof(PowerDeviceTime), 200);
            set => _settings.AddOrUpdateValue(nameof(PowerDeviceTime), value);
        }

        public int BathroomGoingTimes
        {
            get => _settings.GetValueOrDefault(nameof(BathroomGoingTimes), 2);
            set => _settings.AddOrUpdateValue(nameof(BathroomGoingTimes), value);
        }

        public string BathroomFromTime
        {
            get => _settings.GetValueOrDefault(nameof(BathroomFromTime), "00:00:00");
            set => _settings.AddOrUpdateValue(nameof(BathroomFromTime), value);
        }

        public string BathroomToTime
        {
            get => _settings.GetValueOrDefault(nameof(BathroomToTime), "06:00:00");
            set => _settings.AddOrUpdateValue(nameof(BathroomToTime), value);
        }

        public string PersonNotInBedFrom
        {
            get => _settings.GetValueOrDefault(nameof(PersonNotInBedFrom), "00:00:00");
            set => _settings.AddOrUpdateValue(nameof(PersonNotInBedFrom), value);
        }

        public string PersonNotInBedTo
        {
            get => _settings.GetValueOrDefault(nameof(PersonNotInBedTo), "06:00:00");
            set => _settings.AddOrUpdateValue(nameof(PersonNotInBedTo), value);
        }
    }
}
