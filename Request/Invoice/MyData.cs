using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.Invoice;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class MyData: UserProfile
    {

        [JsonProperty(PropertyName = "Logo", NullValueHandling = NullValueHandling.Ignore)]
        public string Logo { get; set; } = null;

        [JsonProperty(PropertyName = "LogoRaw", NullValueHandling = NullValueHandling.Ignore)]
        public Response.ValueLists.Logo[] LogoRaw { get; set; } = null;

        [JsonProperty(PropertyName = "Signature", NullValueHandling = NullValueHandling.Ignore)]
        public string Signature { get; set; } = null;

        [JsonProperty(PropertyName = "SignatureRaw", NullValueHandling = NullValueHandling.Ignore)]
        public Signature SignatureRaw { get; set; } = null;

        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public Country Country { get; set; }

        [JsonProperty(PropertyName = "update_profile", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? UpdateProfile { get; set; } = false;
    }
}
