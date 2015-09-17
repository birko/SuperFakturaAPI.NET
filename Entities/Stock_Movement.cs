using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Stock
    {
        public class Movement
        {
            [JsonProperty(PropertyName = "stock_item_id")]
            public int? StockItemID { get; set; } = null;
            [JsonProperty(PropertyName = "quantity")]
            public decimal Quantity { get; set; } = 1;
            [JsonProperty(PropertyName = "note")]
            public string Note { get; set; }
            [JsonProperty(PropertyName = "created")]
            public DateTime Created { get; set; }
        }
    }
}
