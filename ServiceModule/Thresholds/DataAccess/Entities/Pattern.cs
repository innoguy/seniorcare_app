using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Pattern
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sensors")]
        public List<Sensor> Sensors { get; set; }

        [JsonProperty("time")]
        public Time Time { get; set; }

        [JsonProperty("notified")]
        public Notified Notified { get; set;}

        [JsonProperty("messages")]
        public Messages Messages { get; set; }
    }
}
