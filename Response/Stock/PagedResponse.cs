using Birko.SuperFaktura.Request.Stock;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Response.Stock
{
    public class PagedResponse : PagedResponse<ListItem>
    {
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<ListItem>))]
        public override ItemList<ListItem> Items { get; set; }
    }

    public class PagedLogsResponse : PagedResponse<LogData>
    {
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<LogData>))]
        public override ItemList<LogData> Items { get; set; }
    }
}
