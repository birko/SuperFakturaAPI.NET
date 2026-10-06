using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Other
{
    public class Accounts
    {
        [JsonProperty(PropertyName = "apikey", NullValueHandling = NullValueHandling.Ignore)]
        public string APIKey { get; set; }

        [JsonProperty(PropertyName = "companies", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ListConverter))]
        public IEnumerable<Company> Companies { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "user_name", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

    }
}
