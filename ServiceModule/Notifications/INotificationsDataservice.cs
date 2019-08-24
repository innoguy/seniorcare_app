using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceModule.Notifications.Models;

namespace ServiceModule.Notifications
{
    public interface INotificationsDataservice
    {
        Task<IEnumerable<NotificationEntity>> GetNotifications(string deviceId, string startTime, string endTime);
    }
}
