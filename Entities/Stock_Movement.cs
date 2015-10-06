using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Stock
    {
        public class Movement
        {
            [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
            public int? StockItemID { get; set; } = null;
            [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
            public decimal Quantity { get; set; } = 1;
            [JsonProperty(PropertyName = "note", NullValueHandling = NullValueHandling.Ignore)]
            public string Note { get; set; }
            [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime Created { get; set; }
        }
    }
}
