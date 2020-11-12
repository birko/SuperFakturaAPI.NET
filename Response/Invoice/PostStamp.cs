using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class PostStamp
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
    }
}
