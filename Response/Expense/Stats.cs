using Newtonsoft.Json;
using System;

namespace Birko.SuperFaktura.Response.Expense
{
    public class Stats
    {
        [JsonProperty(PropertyName = "paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Paid { get; set; }

        [JsonProperty(PropertyName = "paid_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? PaidDate { get; set; }

        [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Total { get; set; }
    }
}
