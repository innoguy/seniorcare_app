using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Sensor
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }

        [JsonProperty("cluster_separation")]
        public int ClusterSeparation { get; set; }

        [JsonProperty("duration")]
        public Duration Duration { get; set; }

        [JsonProperty("frequency")]
        public Frequency Frequency { get; set; }
    }
}
