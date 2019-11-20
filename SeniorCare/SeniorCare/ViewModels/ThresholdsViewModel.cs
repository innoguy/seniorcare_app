using System;
using System.Threading;
using System.Threading.Tasks;
using Niko.IoC;
using SeniorCare.BaseClasses;
using SeniorCare.Resources;
using ServiceModule.Thresholds;
using ServiceModule.Thresholds.Models;

namespace SeniorCare.ViewModels
{
    public class ThresholdsViewModel : ViewModelBase
    {
        private Thresholds _thresholds;
        private TimeSpan _televisionFromTime;
        private TimeSpan _televisionToTime;
        private int _powerDeviceTime;
        private int _bathroomGoingTimes;
        private TimeSpan _bathroomFromTime;
        private TimeSpan _bathroomToTime;
        private TimeSpan _personNotInBedFrom;
        private TimeSpan _personNotInBetTo;

        private IThresholdsDataservice _thresholdsDataservice;
        public IThresholdsDataservice ThresholdsDataservice =>
            _thresholdsDataservice ?? (_thresholdsDataservice = AutofacIoC.Resolve<IThresholdsDataservice>());

        public ThresholdsViewModel()
        {
            Title = AppLocalization.ThresholdsPage_Title;

            _thresholds = new Thresholds
            {
                TelevisionFromTime = TimeSpan.Parse(SettingsService.TelevisionFromTime),
                TelevisionToTime = TimeSpan.Parse(SettingsService.TelevisionToTime),
                PowerDeviceTime = SettingsService.PowerDeviceTime,
                BathroomGoingTimes = SettingsService.BathroomGoingTimes,
                BathroomFromTime = TimeSpan.Parse(SettingsService.BathroomFromTime),
                BathroomToTime = TimeSpan.Parse(SettingsService.BathroomToTime),
                PersonNotInBedFrom = TimeSpan.Parse(SettingsService.PersonNotInBedFrom),
                PersonNotInBedTo = TimeSpan.Parse(SettingsService.PersonNotInBedTo)
            };

            InitializeData();
        }

        private void InitializeData()
        {
            _televisionFromTime = TimeSpan.Parse(SettingsService.TelevisionFromTime);
            _televisionToTime = TimeSpan.Parse(SettingsService.TelevisionToTime);
            _powerDeviceTime = SettingsService.PowerDeviceTime;
            _bathroomGoingTimes = SettingsService.BathroomGoingTimes;
            _bathroomFromTime = TimeSpan.Parse(SettingsService.BathroomFromTime);
            _bathroomToTime = TimeSpan.Parse(SettingsService.BathroomToTime);
            _personNotInBedFrom = TimeSpan.Parse(SettingsService.PersonNotInBedFrom);
            _personNotInBetTo = TimeSpan.Parse(SettingsService.PersonNotInBedTo);
        }

        public TimeSpan TelevisionFromTime
        {
            get => _televisionFromTime;
//            set
//            {
//                SetProperty(ref _televisionFromTime, value);
//                _thresholds.TelevisionFromTime = value;
//                SettingsService.TelevisionFromTime = value.ToString();
//                UpdateThresholds();
//            }
        }

        public TimeSpan TelevisionToTime
        {
            get => _televisionToTime;
//            set
//            {
//                SetProperty(ref _televisionToTime, value);
//                _thresholds.TelevisionToTime = value;
//                SettingsService.TelevisionToTime = value.ToString();
//                UpdateThresholds();
//            }
        }

        public int PowerDeviceTime
        {
            get => _powerDeviceTime;
//            set
//            {
//                SetProperty(ref _powerDeviceTime, value);
//                _thresholds.PowerDeviceTime = value;
//                SettingsService.PowerDeviceTime = value;
//                UpdateThresholds();
//            }
        }

        public int BathroomGoingTimes
        {
            get => _bathroomGoingTimes;
//            set
//            {
//                SetProperty(ref _bathroomGoingTimes, value);
//                _thresholds.BathroomGoingTimes = value;
//                SettingsService.BathroomGoingTimes = value;
//                UpdateThresholds();
//            }
        }

        public TimeSpan BathroomFromTime
        {
            get => _bathroomFromTime;
//            set
//            {
//                SetProperty(ref _bathroomFromTime, value);
//                _thresholds.BathroomFromTime = value;
//                SettingsService.BathroomFromTime = value.ToString();
//                UpdateThresholds();
//            }
        }

        public TimeSpan BathroomToTime
        {
            get => _bathroomToTime;
//            set
//            {
//                SetProperty(ref _bathroomToTime, value);
//                _thresholds.BathroomToTime = value;
//                SettingsService.BathroomToTime = value.ToString();
//                UpdateThresholds();
//            }
        }

        public TimeSpan PersonNotInBedFrom
        {
            get => _personNotInBedFrom;
//            set
//            {
//                SetProperty(ref _personNotInBedFrom, value);
//                _thresholds.PersonNotInBedFrom = value;
//                SettingsService.PersonNotInBedFrom = value.ToString();
//                UpdateThresholds();
//            }
        }

        public TimeSpan PersonNotInBedTo
        {
            get => _personNotInBetTo;
//            set
//            {
//                SetProperty(ref _personNotInBetTo, value);
//                _thresholds.PersonNotInBedTo = value;
//                SettingsService.PersonNotInBedTo = value.ToString();
//                UpdateThresholds();
//            }
        }

        private void UpdateThresholds()
        {
            if (!IsInitializing)
                ThresholdsDataservice.UpdateThresholds("device_id_1", _thresholds);
        }

        private async Task BackgroundAsync()
        {
            while (IsInitializing)
            {
                IsBusy = true;

                if (_televisionFromTime == TimeSpan.Parse(SettingsService.TelevisionFromTime)
                    && _televisionToTime == TimeSpan.Parse(SettingsService.TelevisionToTime)
                    && _powerDeviceTime == SettingsService.PowerDeviceTime
                    && _bathroomGoingTimes == SettingsService.BathroomGoingTimes
                    && _bathroomFromTime == TimeSpan.Parse(SettingsService.BathroomFromTime)
                    && _bathroomToTime == TimeSpan.Parse(SettingsService.BathroomToTime)
                    && _personNotInBedFrom == TimeSpan.Parse(SettingsService.PersonNotInBedFrom)
                    && _personNotInBetTo == TimeSpan.Parse(SettingsService.PersonNotInBedTo))
                {
                    await Task.Delay(0);
                    IsInitializing = false;
                    IsBusy = false;
                    break;
                }
            }
        }

        public override void OnAppearing()
        { 
            ThreadPool.QueueUserWorkItem(async o => await BackgroundAsync());
        }

    }
}
