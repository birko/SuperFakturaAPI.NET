using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Tag: Data
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}
