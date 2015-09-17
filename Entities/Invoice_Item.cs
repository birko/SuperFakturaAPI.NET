using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Invoice
    {
        public class Item
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "Description")]
            public string Description { get; set; }
            [JsonProperty(PropertyName = "quantity")]
            public decimal Quantity { get; set; } = 1;
            [JsonProperty(PropertyName = "unit")]
            public string Unit { get; set; } = "ks";
            [JsonProperty(PropertyName = "unit_price")]
            public decimal UnitPrice { get; set; }
            [JsonProperty(PropertyName = "tax")]
            public decimal Tax { get; set; } = 20;
            [JsonProperty(PropertyName = "stock_item_id")]
            public int? StockItemID { get; set; } = null;
            [JsonProperty(PropertyName = "sku")]
            public string SKU { get; set; }
            [JsonProperty(PropertyName = "discount")]
            public decimal Discount { get; set; } = 0;
            [JsonProperty(PropertyName = "load_data_from_stock")]
            public bool LoadDataFromStock { get; set; } = true;
        }
    }
}
