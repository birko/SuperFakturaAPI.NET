using Birko.SuperFaktura.Response.ValueLists;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Other
{
    public class Logos : List<Logo>
    {
        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string URL { get; set; }

        [JsonProperty(PropertyName = "size", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DictionaryConverter<string, string>))]
        public IDictionary<string, string> Size { get; set; }
    }

    public class Company
    {
        [JsonProperty(PropertyName = "Logo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ListConverter))]
        public Logos Logo { get; set; }

        [JsonProperty(PropertyName = "Multiaccount", NullValueHandling = NullValueHandling.Ignore)]
        public MultiAccount MultiAccount { get; set; }

        [JsonProperty(PropertyName = "UserProfile", NullValueHandling = NullValueHandling.Ignore)]
        public AccountUserProfile UserProfile { get; set; }
    }
}
