using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Invoice
{
    public class PagedResponse : PagedResponse<Detail>
    {
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<Detail>))]
        public override ItemList<Detail> Items { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }
    }
}
