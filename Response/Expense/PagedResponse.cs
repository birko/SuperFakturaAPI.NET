using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Response.Expense
{
    public class PagedResponse : PagedResponse<ListItem>
    {
        [JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.ItemListConverter<ListItem>))]
        public override ItemList<ListItem> Items { get; set; }
    }
}
