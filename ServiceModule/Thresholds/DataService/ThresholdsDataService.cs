using System;
using System.Threading.Tasks;
using ServiceModule.Settings;
using ServiceModule.Thresholds.DataAccess;
using ServiceModule.Thresholds.DataAccess.Entities;
using ServiceModule.Thresholds.DataService.Enums;

namespace ServiceModule.Thresholds.DataService
{
    public class ThresholdsDataService : IThresholdsDataService
    {
        private readonly IThresholdsDAL _thresholdsDal;

        public ThresholdsDataService(IThresholdsDAL thresholdsDal)
        {
            _thresholdsDal = thresholdsDal ?? throw new ArgumentNullException(nameof(thresholdsDal));
        }

        public async Task<Models.Thresholds> GetThresholds(string deviceId, double timeStamp)
        {
            try
            {
                var jsonObject = await _thresholdsDal.GetThresholds(deviceId, timeStamp);
                if (jsonObject == null) return null;

                var newThresholds = CreateThresholds(jsonObject);
                return newThresholds;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static Models.Thresholds CreateThresholds(SeniorCareJsonObject jsonObject)
        {
            var televisionPattern = jsonObject.Patterns.Find(p => p.Id == 1);
            var powerDevicePattern = jsonObject.Patterns.Find(p => p.Id == 2);
            var bathroomPattern = jsonObject.Patterns.Find(p => p.Id == 3);
            var personBedSchedulePattern = jsonObject.Patterns.Find(p => p.Id == 4);

            var updatedThresholds = new Models.Thresholds
            {
                TimeStamp = jsonObject.LastModified,
                BathroomFromTime = TimeSpan.Parse(bathroomPattern.Time.Period.Start),
                BathroomToTime = TimeSpan.Parse(bathroomPattern.Time.Period.End),
                BathroomGoingTimes = bathroomPattern.Sensors[0].Frequency.Less,
                PersonNotInBedFrom = TimeSpan.Parse(personBedSchedulePattern.Time.Period.Start),
                PersonNotInBedTo = TimeSpan.Parse(personBedSchedulePattern.Time.Period.End),
                PowerDeviceTime = powerDevicePattern.Sensors[0].Duration.More,
                TelevisionFromTime = TimeSpan.Parse(televisionPattern.Time.Period.Start),
                TelevisionToTime = TimeSpan.Parse(televisionPattern.Time.Period.End)
            };
            return updatedThresholds;

        }
    }
}