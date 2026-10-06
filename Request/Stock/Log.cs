using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Stock
{
    // Stock movement; the item is identified by SKU or StockItemID. Fields left null are not sent.
    public class Log
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }

        // Server uses the current date when not set.
        [JsonProperty(PropertyName = "created", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.DateConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty(PropertyName = "note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty(PropertyName = "purchase_currency", NullValueHandling = NullValueHandling.Ignore)]
        public string PurchaseCurrency { get; set; }

        [JsonProperty(PropertyName = "purchase_unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PurchaseUnitPrice { get; set; }

        [JsonProperty(PropertyName = "purchase_tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PurchaseTax { get; set; }

        // Negative = outgo, positive = income; server default is 1.
        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Quantity { get; set; }

        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }

        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? StockItemID { get; set; }

        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitPrice { get; set; }

        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Tax { get; set; }
    }
}
