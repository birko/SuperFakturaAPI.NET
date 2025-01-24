using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.ValueLists
{
    public class Country
    {
        [JsonProperty(PropertyName = "eu", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool EU { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "iso", NullValueHandling = NullValueHandling.Ignore)]
        public string ISO { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public class CountryData
    {
        [JsonProperty(PropertyName = "Country", NullValueHandling = NullValueHandling.Ignore)]
        public Country Country { get; set; }
    }
}
