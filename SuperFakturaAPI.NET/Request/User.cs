using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request
{
    public class User
    {
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "send_email", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool SendEmail { get; set; }
    }
}
