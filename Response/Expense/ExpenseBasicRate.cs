using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    public class ExpenseBasicRate
    {
        [JsonProperty(PropertyName = "amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Tax { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Total { get; set; }
    }
}
