using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Client
{
    public class Client: Birko.SuperFaktura.Request.Client.Client
    {
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Created { get; set; }
        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Modified { get; set; }
        [JsonProperty(PropertyName = "demo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? Demo { get; set; } = null;
        [JsonProperty(PropertyName = "update", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? Update { get; set; } = null;
        [JsonProperty(PropertyName = "uuid", NullValueHandling = NullValueHandling.Ignore)]
        public string UUID { get; set; }

        [JsonProperty(PropertyName = "Country", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice.Country CountryInfo { get; set; }
        [JsonProperty(PropertyName = "DeliveryCountry", NullValueHandling = NullValueHandling.Ignore)]
        public Invoice.Country DeliveryCountryInfo { get; set; }
    }
}
