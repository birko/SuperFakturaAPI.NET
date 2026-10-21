using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Request.Expense
{
    public class AccountingDetail
    {
        [JsonProperty(PropertyName = "analytics_account", NullValueHandling = NullValueHandling.Ignore)]
        public string AnalyticsAccount { get; set; }

        [JsonProperty(PropertyName = "operation", NullValueHandling = NullValueHandling.Ignore)]
        public string Operation { get; set; }

        [JsonProperty(PropertyName = "order", NullValueHandling = NullValueHandling.Ignore)]
        public string Order { get; set; }

        [JsonProperty(PropertyName = "place", NullValueHandling = NullValueHandling.Ignore)]
        public string Place { get; set; }

        [JsonProperty(PropertyName = "preconfidence", NullValueHandling = NullValueHandling.Ignore)]
        public string PreConfidence { get; set; }

        [JsonProperty(PropertyName = "synthetic_account", NullValueHandling = NullValueHandling.Ignore)]
        public string SyntheticAccount { get; set; }

        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }

    public class ExpenseItem
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public int? ID { get; set; }

        [JsonProperty(PropertyName = "AccountingDetail", NullValueHandling = NullValueHandling.Ignore)]
        public AccountingDetail AccountingDetail { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Quantity { get; set; }

        [JsonProperty(PropertyName = "tax", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Tax { get; set; }

        [JsonProperty(PropertyName = "unit_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnitPrice { get; set; }
    }
}
