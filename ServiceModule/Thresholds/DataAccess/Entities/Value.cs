﻿using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Value
    {
        [JsonProperty("more")]
        public int More { get; set; }

        [JsonProperty("less")]
        public int Less { get; set; }

        [JsonProperty("equal")]
        public string Equal { get; set; }
    }
}
