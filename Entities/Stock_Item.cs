using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Stock
    {
        public class Item
        {
            [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
            public int? ID { get; internal set; } = null;
            [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
            public string Description { get; set; }
            [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Ignore)]
            public string Unit { get; set; } = "ks";
            [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
            public decimal UnitPrice { get; set; }
            [JsonProperty(PropertyName = "vat", NullValueHandling = NullValueHandling.Ignore)]
            public decimal VAT { get; set; } = 20;
            [JsonProperty(PropertyName = "stock", NullValueHandling = NullValueHandling.Ignore)]
            public decimal Stock { get; set; } = 0;
            [JsonProperty(PropertyName = "sku", NullValueHandling = NullValueHandling.Ignore)]
            public string SKU { get; set; }
        }
    }
}
