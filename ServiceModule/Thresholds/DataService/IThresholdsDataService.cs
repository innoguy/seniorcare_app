using System.Threading.Tasks;

namespace ServiceModule.Thresholds.DataService
{
    public interface IThresholdsDataService
    {
        Task<Models.Thresholds> GetThresholds(string deviceId, double timeStamp);
    }
}
