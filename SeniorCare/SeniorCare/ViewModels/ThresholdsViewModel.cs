using System;
using System.Threading;
using System.Threading.Tasks;
using Niko.IoC;
using SeniorCare.BaseClasses;
using SeniorCare.Resources;
using ServiceModule.Thresholds.DataService;
using ServiceModule.Thresholds.DataService.Models;
using Xamarin.Forms;

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
        private TimeSpan _personNotInBedTo;

        private IThresholdsDataService _thresholdsDataservice;
        public IThresholdsDataService ThresholdsDataservice =>
            _thresholdsDataservice ?? (_thresholdsDataservice = AutofacIoC.Resolve<IThresholdsDataService>());

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
            _personNotInBedTo = TimeSpan.Parse(SettingsService.PersonNotInBedTo);
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
            get => _personNotInBedTo;
//            set
//            {
//                SetProperty(ref _personNotInBetTo, value);
//                _thresholds.PersonNotInBedTo = value;
//                SettingsService.PersonNotInBedTo = value.ToString();
//                UpdateThresholds();
//            }
        }

        private async Task UpdateThresholds()
        {
            while (true)
            {
                var thresholds = await ThresholdsDataservice.GetThresholds("device_id_1", _thresholds);
                if (thresholds != null)
                {
                    await UpdateData(thresholds);
                }
                
                await Task.Delay(1000);
            }
        }

        private async Task UpdateData(Thresholds thresholds)
        {
            _thresholds.TelevisionFromTime = thresholds.TelevisionFromTime;
            _thresholds.TelevisionToTime = thresholds.TelevisionToTime;
            _thresholds.PowerDeviceTime = thresholds.PowerDeviceTime;
            _thresholds.BathroomGoingTimes = thresholds.BathroomGoingTimes;
            _thresholds.BathroomFromTime = thresholds.BathroomFromTime;
            _thresholds.BathroomToTime = thresholds.BathroomToTime;
            _thresholds.PersonNotInBedFrom = thresholds.PersonNotInBedFrom;
            _thresholds.PersonNotInBedTo = thresholds.PersonNotInBedTo;

            await Device.InvokeOnMainThreadAsync(() =>
            {
                _televisionFromTime = thresholds.TelevisionFromTime;
                _televisionToTime = thresholds.TelevisionToTime;
                _powerDeviceTime = thresholds.PowerDeviceTime / 60;
                _bathroomGoingTimes = thresholds.BathroomGoingTimes;
                _bathroomFromTime = thresholds.BathroomFromTime;
                _bathroomToTime = thresholds.BathroomToTime;
                _personNotInBedFrom = thresholds.PersonNotInBedFrom;
                _personNotInBedTo = thresholds.PersonNotInBedTo;
            });
        }

        public override void OnAppearing()
        {
            //            ThreadPool.QueueUserWorkItem(async o => await BackgroundAsync());
            ThreadPool.QueueUserWorkItem(async o => await UpdateThresholds());
        }


        //        private async Task BackgroundAsync()
        //        {
        //            while (IsInitializing)
        //            {
        //                IsBusy = true;
        //
        //                if (_televisionFromTime == TimeSpan.Parse(SettingsService.TelevisionFromTime)
        //                    && _televisionToTime == TimeSpan.Parse(SettingsService.TelevisionToTime)
        //                    && _powerDeviceTime == SettingsService.PowerDeviceTime
        //                    && _bathroomGoingTimes == SettingsService.BathroomGoingTimes
        //                    && _bathroomFromTime == TimeSpan.Parse(SettingsService.BathroomFromTime)
        //                    && _bathroomToTime == TimeSpan.Parse(SettingsService.BathroomToTime)
        //                    && _personNotInBedFrom == TimeSpan.Parse(SettingsService.PersonNotInBedFrom)
        //                    && _personNotInBedTo == TimeSpan.Parse(SettingsService.PersonNotInBedTo))
        //                {
        //                    await Task.Delay(0);
        //                    IsInitializing = false;
        //                    IsBusy = false;
        //                    break;
        //                }
        //            }
        //        }



        /*
        private void UpdateThresholds()
        {
            if (!IsInitializing)
                ThresholdsDataservice.UpdateThresholds("device_id_1", _thresholds);
        }
        */

    }
}
