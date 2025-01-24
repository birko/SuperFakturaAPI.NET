using Birko.SuperFaktura.Response;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class MyData: UserProfile
    {
        [JsonProperty(PropertyName = "update_profile", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? UpdateProfile { get; set; } = false;
    }
}
