using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Client
    {
        public class ContactPerson
        {
            [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
            public int ClientID { get; set; }
            [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
            public string Email { get; set; }
        }
    }
}
