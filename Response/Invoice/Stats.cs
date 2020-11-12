using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class Stats
    {
        [JsonProperty(PropertyName = "send_by", NullValueHandling = NullValueHandling.Ignore)]
        public string SendBy { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Total { get; set; }
    }
}
