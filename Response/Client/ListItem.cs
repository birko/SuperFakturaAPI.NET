using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Birko.SuperFaktura.Response.Client
{
    [JsonConverter(typeof(ClientItemConverter))]
    public class ListItem : List<Stats>
    {
        [JsonProperty(PropertyName = "Client", NullValueHandling = NullValueHandling.Ignore)]
        public Client Client { get; set; } = null;
    }
}
