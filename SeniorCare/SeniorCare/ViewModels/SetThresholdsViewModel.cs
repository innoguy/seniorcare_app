using System;
using System.Collections.Generic;
using System.Text;
using SeniorCare.BaseClasses;
using SeniorCare.Resources;
using ServiceModule.Thresholds.DataService.Models;

namespace SeniorCare.ViewModels
{
    public class SetThresholdsViewModel : ViewModelBase
    {
        private Thresholds _thresholds;
        private TimeSpan _televisionFromTime;
        private TimeSpan _televisionToTime;
        private int _powerDeviceTime;

        public SetThresholdsViewModel()
        {
            Title = AppLocalization.ThresholdsPage_Title;

            _thresholds = new Thresholds
            {
                TelevisionFromTime = TimeSpan.Parse(SettingsService.TelevisionFromTime),
                TelevisionToTime = TimeSpan.Parse(SettingsService.TelevisionToTime),
                PowerDeviceTime = SettingsService.PowerDeviceTime
            };
        }

        private void InitializeData()
        {
            _televisionFromTime = TimeSpan.Parse(SettingsService.TelevisionFromTime);
            _televisionToTime = TimeSpan.Parse(SettingsService.TelevisionToTime);
            _powerDeviceTime = SettingsService.PowerDeviceTime;
        }

        public TimeSpan TelevisionFromTime
        {
            get => _televisionFromTime;
            set => SetProperty(ref _televisionFromTime, value);
        }

        public TimeSpan TelevisionToTime
        {
            get => _televisionToTime;
            set => SetProperty(ref _televisionToTime, value);
        }

        public int PowerDeviceTime
        {
            get => _powerDeviceTime;
            set => SetProperty(ref _powerDeviceTime, value);
        }
    }
}
