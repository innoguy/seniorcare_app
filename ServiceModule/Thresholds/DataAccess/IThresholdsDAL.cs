using System.Threading.Tasks;
using ServiceModule.Thresholds.DataAccess.Entities;

namespace ServiceModule.Thresholds.DataAccess
{
    public interface IThresholdsDAL
    {
        Task<SeniorCareJsonObject> GetThresholds(string deviceId, double timeStamp);
    }
}
