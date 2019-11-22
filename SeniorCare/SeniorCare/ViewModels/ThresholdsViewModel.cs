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

        public int BathroomGoingTimes
        {
            get => _bathroomGoingTimes;
            set => SetProperty(ref _bathroomGoingTimes, value);
        }

        public TimeSpan BathroomFromTime
        {
            get => _bathroomFromTime;
            set => SetProperty(ref _bathroomFromTime, value);
        }

        public TimeSpan BathroomToTime
        {
            get => _bathroomToTime;
            set => SetProperty(ref _bathroomToTime, value);

        }

        public TimeSpan PersonNotInBedFrom
        {
            get => _personNotInBedFrom;
            set => SetProperty(ref _personNotInBedFrom, value);
        }

        public TimeSpan PersonNotInBedTo
        {
            get => _personNotInBedTo;
            set => SetProperty(ref _personNotInBedTo, value);
        }

        private async Task UpdateData()
        {
            while (true)
            {
                var thresholds = await ThresholdsDataservice.GetThresholds("device_id_1", _thresholds.TimeStamp);
                if (thresholds != null)
                {
                    await UpdateThresholds(thresholds);
                    UpdateCache(thresholds);
                }

                await Task.Delay(1000);
            }
        }

        private async Task UpdateThresholds(Thresholds thresholds)
        {
            _thresholds.TimeStamp = thresholds.TimeStamp;
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
                TelevisionFromTime = thresholds.TelevisionFromTime;
                TelevisionToTime = thresholds.TelevisionToTime;
                PowerDeviceTime = thresholds.PowerDeviceTime / 60;
                BathroomGoingTimes = thresholds.BathroomGoingTimes;
                BathroomFromTime = thresholds.BathroomFromTime;
                BathroomToTime = thresholds.BathroomToTime;
                PersonNotInBedFrom = thresholds.PersonNotInBedFrom;
                PersonNotInBedTo = thresholds.PersonNotInBedTo;
            });
        }

        private void UpdateCache(Thresholds thresholds)
        {
            SettingsService.TimeStamp = thresholds.TimeStamp;
            SettingsService.TelevisionFromTime = thresholds.TelevisionFromTime.ToString();
            SettingsService.TelevisionToTime = thresholds.TelevisionToTime.ToString();
            SettingsService.PowerDeviceTime = thresholds.PowerDeviceTime;
            SettingsService.BathroomGoingTimes = thresholds.BathroomGoingTimes;
            SettingsService.BathroomFromTime = thresholds.BathroomFromTime.ToString();
            SettingsService.BathroomToTime = thresholds.BathroomToTime.ToString();
            SettingsService.PersonNotInBedFrom = thresholds.PersonNotInBedFrom.ToString();
            SettingsService.PersonNotInBedTo = thresholds.PersonNotInBedTo.ToString();
        }

        public override void OnAppearing()
        {
            ThreadPool.QueueUserWorkItem(async o => await UpdateData());
        }
    }
}
