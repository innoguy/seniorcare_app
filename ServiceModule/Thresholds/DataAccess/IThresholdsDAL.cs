using System.Threading.Tasks;
using ServiceModule.Thresholds.DataAccess.Entities;

namespace ServiceModule.Thresholds.DataAccess
{
    public interface IThresholdsDAL
    {
        void UpdateThresholds(string deviceId, DataService.Models.Thresholds thresholds);

        Task<SeniorCareJsonObject> GetThresholds(string deviceId, string md5Hash);
    }
}
