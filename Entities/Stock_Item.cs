using Raven.Imports.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birko.SuperFaktura.Entities
{
    public partial class Stock
    {
        public class Item
        {
            [JsonProperty(PropertyName = "id")]
            public int? ID { get; internal set; } = null;
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            [JsonProperty(PropertyName = "description")]
            public string Description { get; set; }
            [JsonProperty(PropertyName = "unit")]
            public string Unit { get; set; } = "ks";
            [JsonProperty(PropertyName = "unit_price")]
            public decimal UnitPrice { get; set; }
            [JsonProperty(PropertyName = "vat")]
            public decimal VAT { get; set; } = 20;
            [JsonProperty(PropertyName = "stock")]
            public decimal Stock { get; set; } = 0;
            [JsonProperty(PropertyName = "sku")]
            public string SKU { get; set; }
        }
    }
}
