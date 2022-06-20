using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Stock
{
    public class Log
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int StockItemID { get; internal set; }
        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }
        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Quantity { get; set; }
        [JsonProperty(PropertyName = "note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
