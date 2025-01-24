using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    public class MyData : UserProfile
    {

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "vat_interval", NullValueHandling = NullValueHandling.Ignore)]
        public string VATInterval { get; set; }
    }
}