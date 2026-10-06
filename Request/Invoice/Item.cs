using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    // Fields left null are not sent; server defaults: discount 0, quantity 1, load_data_from_stock 0.
    public class Item
    {
        [JsonProperty(PropertyName = "AccountingDetail", NullValueHandling = NullValueHandling.Ignore)]
        public AccountingDetail AccountingDetail { get; set; } = null;

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "discount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Discount { get; set; }

        [JsonProperty(PropertyName = "discount_description", NullValueHandling = NullValueHandling.Ignore)]
        public string DiscountDescription { get; set; }

        [JsonProperty(PropertyName = "load_data_from_stock", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? LoadDataFromStock { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Quantity { get; set; }

        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }

        [JsonProperty(PropertyName = "stock_item_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? StockItemID { get; set; } = null;

        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Tax { get; set; }

        [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitPrice { get; set; }

        [JsonProperty(PropertyName = "use_document_currency", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringBooleanConverter))]
        public bool? UseDocumentCurrency { get; set; }
    }
}
