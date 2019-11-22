using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Time
    {
        [JsonProperty("period")]
        public Period Period { get; set; }

        [JsonProperty("frequency")]
        public int Frequency { get; set; }
    }
}
