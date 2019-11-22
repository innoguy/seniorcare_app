﻿using Newtonsoft.Json;

namespace ServiceModule.Thresholds.DataAccess.Entities
{
    public class Duration
    {
        [JsonProperty("more")]
        public int More { get; set; }

        [JsonProperty("less")]
        public int Less { get; set; }
    }
}
