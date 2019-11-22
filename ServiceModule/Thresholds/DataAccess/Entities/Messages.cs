using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Messages
    {
        [JsonProperty("rule")]
        public string Rule { get; set; }
    }
}
