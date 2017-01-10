using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Country
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; internal set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "iso", NullValueHandling = NullValueHandling.Ignore)]
        public string ISO { get; set; }
        [JsonProperty(PropertyName = "eu", NullValueHandling = NullValueHandling.Ignore)]
        public int EU { get; set; }
    }
}
