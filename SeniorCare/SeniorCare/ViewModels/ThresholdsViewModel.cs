using SeniorCare.BaseClasses;
using SeniorCare.Resources;

namespace SeniorCare.ViewModels
{
    public class ThresholdsViewModel : ViewModelBase
    {
        private string _televisiomFromTime;
        private string _televisionToTime;
        private int _powerDeviceTime;
        private int _bathroomGoingTimes;
        private string _bathroomFromTime;
        private string _bathroomToTime;
        private string _personNotInBetFrom;
        private string _personNotInBetTo;

        public ThresholdsViewModel()
        {
            Title = AppLocalization.ThresholdsPage_Title;
        }

        public string TelevisiomFromTime
        {
            get => _televisiomFromTime;
            set => SetProperty(ref _televisiomFromTime, value);
        }

        public string TelevisionUntilTime
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

        public string BathroomFromTime
        {
            get => _bathroomFromTime;
            set => SetProperty(ref _bathroomFromTime, value);
        }

        public string BathroomUntilTime
        {
            get => _bathroomToTime;
            set => SetProperty(ref _bathroomToTime, value);
        }

        public string PersonNotInBetFrom
        {
            get => _personNotInBetFrom;
            set => SetProperty(ref _personNotInBetFrom, value);
        }

        public string PersonNotInBetUntil
        {
            get => _personNotInBetTo;
            set => SetProperty(ref _personNotInBetTo, value);
        }
    }
}
