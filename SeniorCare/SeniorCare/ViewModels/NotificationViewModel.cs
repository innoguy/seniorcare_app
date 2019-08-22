using SeniorCare.BaseClasses;

namespace SeniorCare.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        public string RuleId { get; }
        public string Controllerid { get; }
        public string Time { get; }
        public string Message { get; }

        public NotificationViewModel(string ruleId, string controllerId, string time, string message)
        {
            RuleId = ruleId;
            Controllerid = controllerId;
            Time = time;
            Message = message;
        }
    }
}
