using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class SeniorCareJsonObject
    {
        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("last-modified")]
        public double LastModified { get; set; }

        [JsonProperty("patterns")]
        public List<Pattern> Patterns { get; set; }
    }
}
