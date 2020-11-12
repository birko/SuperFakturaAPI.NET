using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class FlashMessage
    {
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}
