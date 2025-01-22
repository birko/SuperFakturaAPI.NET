using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Expense
{
    public class ExpenseItem
    {

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax { get; set; }

        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal UnitPrice { get; set; }
    }
}
