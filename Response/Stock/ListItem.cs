using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Stock
{
    public class ListItem
    {
        [JsonProperty(PropertyName = "StockItem", NullValueHandling = NullValueHandling.Ignore)]
        public Item StockItem { get; set; } = null;
    }
}
