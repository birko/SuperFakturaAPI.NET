using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Stock
{
    public class Log
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }

        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; } = DateTime.Now;

        [JsonProperty(PropertyName = "note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty(PropertyName = "purchase_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string PurchaseCurrency { get; set; }

        [JsonProperty(PropertyName = "purchase_unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PurchaseUnitPrice{ get; set; }

        [JsonProperty(PropertyName = "purchase_tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PurchaseTax { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }

        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int StockItemID { get; internal set; }

        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPrice { get; set; }

        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax { get; set; }
    }
}
