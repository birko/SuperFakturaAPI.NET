using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Client
{
    public class ContactPerson : Birko.SuperFaktura.Request.Client.ContactPerson
    {
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }
        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public string UserProfileID { get; set; }
    }
}
