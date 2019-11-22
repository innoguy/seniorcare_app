using System;
using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Period
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }
    }
}
