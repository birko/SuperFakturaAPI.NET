using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Client
{
    public class PagedResponse : PagedResponse<ListItem>
    {
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<ListItem>))]
        public override ItemList<ListItem> Items { get; set; }
    }
}
