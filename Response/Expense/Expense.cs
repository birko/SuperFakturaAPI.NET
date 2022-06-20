using Birko.SuperFaktura.Converters;
using Newtonsoft.Json;
using System;
using System.Dynamic;

namespace Birko.SuperFaktura.Response.Expense
{
    public class Expense : Request.Expense.Expense
    {
        [JsonProperty(PropertyName = "amount_paid", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AmountPaid { get; set; }
       
        [JsonProperty(PropertyName = "demo", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringBooleanConverter))]
        public bool? Demo { get; set; } = null;
        [JsonProperty(PropertyName = "exchange_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; }
        [JsonProperty(PropertyName = "flag", NullValueHandling = NullValueHandling.Ignore)]
        public string Flag { get; set; }
        [JsonProperty(PropertyName = "modified", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Modified { get; set; }
        [JsonProperty(PropertyName = "paydare", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Paydate { get; set; }
        [JsonProperty(PropertyName = "recuring", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Recuring { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public int Status { get; set; }
        [JsonProperty(PropertyName = "taxable_suply", NullValueHandling = NullValueHandling.Ignore)]
        public ExpandoObject TaxableSuply { get; set; }
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserID { get; set; }
        [JsonProperty(PropertyName = "user_profile_id", NullValueHandling = NullValueHandling.Ignore)]
        public int? UserProfileID { get; set; }
    }
}
