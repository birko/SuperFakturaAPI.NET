using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Stock
{
    public class Log
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int StockItemID { get; internal set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }
        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPrice { get; set; }
        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT { get; set; } = 20;
        [JsonProperty(PropertyName = "stock", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Stock { get; set; }

    }
}
