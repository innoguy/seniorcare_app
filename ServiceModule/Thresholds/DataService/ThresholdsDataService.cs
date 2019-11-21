using System;
using System.Collections.Generic;
using System.Text;
using ServiceModule.Thresholds.DataAccess;

namespace ServiceModule.Thresholds.DataService
{
    public class ThresholdsDataService : IThresholdsDataService
    {
        private readonly IThresholdsDAL _thresholdsDal;

        public ThresholdsDataService(IThresholdsDAL thresholdsDal)
        {
            _thresholdsDal = thresholdsDal ?? throw new ArgumentNullException(nameof(thresholdsDal));
        }
    }
}
