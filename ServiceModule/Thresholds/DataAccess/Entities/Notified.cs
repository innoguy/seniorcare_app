using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Notified
    {
        [JsonProperty("duration")]
        public int Duration { get; set; }
    }
}
