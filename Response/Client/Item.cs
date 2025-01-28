using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Client
{
    public class Item
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client Client { get; set; } = null;

        [JsonProperty(PropertyName = "ClientStat", NullValueHandling = NullValueHandling.Ignore)]
        public Stat ClientStat { get; set; } = null;
    }
}
