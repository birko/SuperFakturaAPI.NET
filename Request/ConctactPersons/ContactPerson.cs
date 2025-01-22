using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.ContactPersons
{
    public class ContactPerson
    {
        [JsonProperty(PropertyName = "client_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ClientID { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
    }
}
