using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Value
    {
        [JsonProperty("more")]
        public float More { get; set; }

        [JsonProperty("less")]
        public float Less { get; set; }

        [JsonProperty("equal")]
        public string Equal { get; set; }
    }
}
