using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServiceModule.Notifications.Models;
using ServiceModule.Settings;

namespace ServiceModule.Notifications
{
    public class NotificationsDataservice : INotificationsDataservice
    {
        private readonly ISettingsService _settingsService;
        static readonly HttpClient _client = new HttpClient();
        private static string _baseURL;

        public NotificationsDataservice(ISettingsService settingsService)
        {
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        }


        public async Task<IEnumerable<NotificationEntity>> GetNotifications(string deviceId, long startTime)
        {
            var notificationsList = new List<NotificationEntity>();
            try
            {
                _baseURL = $"{_settingsService.Protocol}://{_settingsService.IpAddress}:{_settingsService.Port}/apiv1";
                var response = await _client.GetAsync($"{_baseURL}/notification/query/{deviceId}/{startTime}");
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
