using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Stock
{
    public class Item: Data
    {
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "hide_in_autocomplete", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HideInAutoComplete { get; set; }
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; internal set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
        public string SKU { get; set; }
        [JsonProperty(PropertyName = "stock", NullValueHandling = NullValueHandling.Ignore)]
        public int Stock { get; set; }
        [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }
        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPrice { get; set; }
        [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
        public decimal VAT { get; set; }
        [JsonProperty(PropertyName = "watch_stock", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool WatchStock { get; set; }
    }
}
