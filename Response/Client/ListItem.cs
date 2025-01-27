using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Client
{
    public class ListItem
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client Client { get; set; } = null;

        [JsonProperty(PropertyName = "ClientStat", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject ClientStat { get; set; } = null;
    }
}
