using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Export
{

    public class Export
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "progress", NullValueHandling = NullValueHandling.Ignore)]
        public int Progress { get; set; }

        [JsonProperty(PropertyName = "count_total", NullValueHandling = NullValueHandling.Ignore)]
        public int CountTotal { get; set; }

        [JsonProperty(PropertyName = "count_completed", NullValueHandling = NullValueHandling.Ignore)]
        public int CountCompleted { get; set; }
    }
}
