using Newtonsoft.Json;

namespace Birko.SuperFaktura.Request.Invoice
{
    public class AccountingDetail
    {
        [JsonProperty(PropertyName = "place", NullValueHandling = NullValueHandling.Ignore)]
        public string Place { get; set; }
        [JsonProperty(PropertyName = "order", NullValueHandling = NullValueHandling.Ignore)]
        public string Order { get; set; }
        [JsonProperty(PropertyName = "type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; } = ValueLists.AccountingDetailType.Item;
        [JsonProperty(PropertyName = "operation", NullValueHandling = NullValueHandling.Ignore)]
        public string Operation { get; set; }
        [JsonProperty(PropertyName = "analytics_account", NullValueHandling = NullValueHandling.Ignore)]
        public string AnalyticsAccount { get; set; }
        [JsonProperty(PropertyName = "synthetic_account", NullValueHandling = NullValueHandling.Ignore)]
        public string SyntheticAccount { get; set; }
        [JsonProperty(PropertyName = "preconfidence", NullValueHandling = NullValueHandling.Ignore)]
        public string PreConfidence { get; set; }
    }
}
