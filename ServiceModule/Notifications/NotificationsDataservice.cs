using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceModule.Notifications.Models;

namespace ServiceModule.Notifications
{
    public class NotificationsDataservice : INotificationsDataservice
    {
        static readonly HttpClient _client = new HttpClient();
        private static string _baseURL = "http://5.2.158.223:45678/apiv1";

        public async Task<IEnumerable<NotificationEntity>> GetNotifications(string deviceId, string startTime, string endTime)
        {
            var notificationsList = new List<NotificationEntity>();
            try
            {
                var response = await _client.GetAsync($"{_baseURL}/notification/query/{deviceId}/{startTime}/{endTime}");
                if (response.IsSuccessStatusCode)
                {
                    var resp = await response.Content.ReadAsStringAsync();
                    notificationsList = JsonConvert.DeserializeObject<List<NotificationEntity>>(resp);
                }

                return notificationsList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return notificationsList;
            }
            
        }
    }
}
