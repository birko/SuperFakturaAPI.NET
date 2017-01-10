using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class Item
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Quantity { get; set; } = 1;
        [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; } = "ks";
        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPrice { get; set; }
        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax { get; set; } = 20;
        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? StockItemID { get; set; } = null;
        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }
        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Discount { get; set; } = 0;
        [JsonProperty(PropertyName = "discount_description", NullValueHandling = NullValueHandling.Ignore)]
        public string DiscountDescription { get; set; }
        [JsonProperty(PropertyName = "load_data_from_stock", NullValueHandling = NullValueHandling.Ignore)]
        public bool LoadDataFromStock { get; set; } = true;
    }
}
