using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response
{
    public class Subscription
    {
        [JsonProperty(PropertyName = "create_trial", NullValueHandling = NullValueHandling.Ignore)]
        public bool CreateTrial { get; set; }
    }
}
