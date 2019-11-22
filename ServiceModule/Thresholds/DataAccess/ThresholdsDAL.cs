using System;
using System.Threading.Tasks;
using ServiceModule.RestHttpClient;
using ServiceModule.Settings;
using ServiceModule.Thresholds.DataAccess.Entities;

namespace ServiceModule.Thresholds.DataAccess
{
    public class ThresholdsDAL : RestClientBase, IThresholdsDAL
    {
        public ThresholdsDAL(ISettingsService settingsService) : base(settingsService) { }

        public async Task<SeniorCareJsonObject> GetThresholds(string deviceId, double timeStamp)
            => await GetAsync<SeniorCareJsonObject>($"apiv1/config/patterns/download/{deviceId}/{timeStamp}");
    }
}
