using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response
{
    public class Register
    {
        [JsonProperty(PropertyName = "Subscription", NullValueHandling = NullValueHandling.Ignore)]
        public Subscription Subscription { get; set; }
        [JsonProperty(PropertyName = "User", NullValueHandling = NullValueHandling.Ignore)]
        public User User { get; set; }
    }
}
