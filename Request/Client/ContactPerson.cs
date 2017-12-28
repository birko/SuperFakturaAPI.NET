using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Client
{
    public class ContactPerson
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ClientID { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
    }
}
